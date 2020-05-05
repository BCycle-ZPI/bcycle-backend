using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Data;
using bcycle_backend.Models.Entities;
using bcycle_backend.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace bcycle_backend.Services
{
    public class GroupTripService
    {
        private const int CodeLength = 6;

        private readonly DbSet<GroupTrip> _trips;
        private readonly DbSet<GroupTripPoint> _points;
        private readonly BCycleContext _dbContext;

        public GroupTripService(BCycleContext context)
        {
            _trips = context.GroupTrips;
            _points = context.GroupTripPoints;
            _dbContext = context;
        }

        public async Task<GroupTrip> CreateAsync(GroupTripRequest data, string subjectId)
        {
            SetOrder(data.Route);

            var trip = new GroupTrip
            {
                Name = data.Name,
                Description = data.Description,
                HostId = subjectId,
                TripCode = GenerateCode(),
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Route = data.Route
            };

            await _trips.AddAsync(trip);
            await _dbContext.SaveChangesAsync();
            return trip;
        }
        
        private string GenerateCode()
        {
            string code;
            do
            {
                code = Guid.NewGuid().ToString().Substring(0, CodeLength);
            } while (_trips.Any(t => t.TripCode == code));
            
            return code;
        }

        public async Task<GroupTrip> UpdateAsync(int tripId, GroupTripRequest data, string subjectId)
        {
            var trip = await FindActiveAsync(tripId, subjectId);
            if (trip == null) return null;

            _points.RemoveRange(trip.Route);
            SetOrder(data.Route);
            trip.Update(data);
            await _dbContext.SaveChangesAsync();
            return trip;
        }

        private void SetOrder(List<GroupTripPoint> route)
        {
            for (int i = 0; i < route.Count; i++)
            {
                route[i].Ordinal = i;
            }
        }

        public async Task<GroupTrip> FindAsync(int id, string subjectId)
        {
            var trip = await _trips
                .Where(t => t.Id == id)
                .Include(t => t.Participants)
                .Include(t => t.Route)
                .FirstOrDefaultAsync();

            if (trip == null) return null;
            if (trip.HostId == subjectId) return trip;

            var participant = trip.FindAmongAccepted(subjectId);
            if (participant == null) return null;

            trip.Participants = trip.Participants.Where(p => p.Status == ParticipantStatus.Accepted).ToList();
            return trip;
        }

        public async Task<List<GroupTrip>> FindAllUserTripsAsync(string userId)
        {
            var trips = await _trips
                .Include(t => t.Participants)
                .Include(t => t.Route)
                .Where(t => t.HostId == userId || t.Participants.Exists(p => p.UserId == userId && p.Status == ParticipantStatus.Accepted))
                .ToListAsync();

            return trips.Select(trip =>
            {
                if (trip.HostId != userId)
                {
                    trip.Participants = trip.Participants.Where(p => p.Status == ParticipantStatus.Accepted).ToList();
                }
                return trip;
            }).ToList();
        }

        public async Task<GroupTrip> RemoveAsync(int tripId, string subjectId)
        {
            var trip = await _trips
                .Where(gt => gt.Id == tripId)
                .Where(gt => gt.HostId == subjectId)
                .FirstOrDefaultAsync();

            if (trip == null) return null;

            _trips.Remove(trip);
            await _dbContext.SaveChangesAsync();
            return trip;
        }

        public async Task<GroupTrip> Join(string subjectId, string code)
        {
            var trip = await _trips
                .Where(gt => gt.StartDate > DateTime.Now)
                .Where(gt => gt.TripCode == code)
                .Include(t => t.Participants)
                .FirstOrDefaultAsync();
            if (trip == null) return null;

            var existingUser = trip.Participants.FirstOrDefault(p => p.UserId == subjectId);
            if (existingUser != null)
            {
                if (existingUser.Status == ParticipantStatus.Accepted) return null;
                existingUser.Status = ParticipantStatus.Pending;
            }
            else
            {
                var participant = new GroupTripParticipant
                {
                    Status = ParticipantStatus.Pending, GroupTripId = trip.Id, UserId = subjectId
                };
                trip.Participants.Add(participant);
            }

            await _dbContext.SaveChangesAsync();
            return trip;
        }

        public async Task<GroupTripParticipant> AcceptRequestAsync(int tripId, string userId, string subjectId)
        {
            var trip = await FindActiveAsync(tripId, subjectId);
            var participant = trip?.FindAmongPending(userId);
            if (participant == null) return null;

            participant.Status = ParticipantStatus.Accepted;
            await _dbContext.SaveChangesAsync();
            return participant;
        }

        public async Task<GroupTripParticipant> RejectRequestAsync(int tripId, string userId, string subjectId)
        {
            var trip = await FindActiveAsync(tripId, subjectId);
            var participant = trip?.FindAmongPending(userId);
            if (participant == null) return null;

            participant.Status = ParticipantStatus.Rejected;
            await _dbContext.SaveChangesAsync();
            return participant;
        }

        public async Task<GroupTripParticipant> RemoveParticipant(int tripId, string userId, string subjectId)
        {
            var trip = await FindActiveAsync(tripId, subjectId);
            var participant = trip?.FindAmongAccepted(userId);
            if (participant == null) return null;

            participant.Status = ParticipantStatus.Removed;
            await _dbContext.SaveChangesAsync();
            return participant;
        }

        private Task<GroupTrip> FindActiveAsync(int tripId, string hostId) =>
            _trips
                .Where(gt => gt.StartDate > DateTime.Now)
                .Where(gt => gt.Id == tripId)
                .Where(gt => gt.HostId == hostId)
                .Include(t => t.Participants)
                .Include(t => t.Route)
                .FirstOrDefaultAsync();
    }
}
