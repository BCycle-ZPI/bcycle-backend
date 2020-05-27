using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Data;
using bcycle_backend.Models.Entities;
using bcycle_backend.Models.Requests;
using Microsoft.EntityFrameworkCore;
using static bcycle_backend.ProjectConstants;

namespace bcycle_backend.Services
{
    public class TripService
    {
        private readonly DbSet<Trip> _tripsDbSet;
        private readonly BCycleContext _dbContext;

        public TripService(BCycleContext context)
        {
            _tripsDbSet = context.Trips;
            _dbContext = context;
        }

        public IQueryable<Trip> GetAll(string userId) =>
            _tripsDbSet
                .Where(t => t.UserId == userId)
                .Include(t => t.Photos)
                .Include(t => t.Route)
                .OrderByDescending(t => t.Started);

        public Task<Trip> GetUserTripAsync(int tripId, string userId) =>
            _tripsDbSet
                .Where(t => t.Id == tripId)
                .Where(t => t.UserId == userId)
                .Include(t => t.Photos)
                .Include(t => t.Route)
                .FirstOrDefaultAsync();

        public Task<Trip> GetPublicTripAsync(Guid guid) =>
            _tripsDbSet
                .Where(t => t.SharingGuid == guid)
                .Include(t => t.Photos)
                .Include(t => t.Route)
                .FirstOrDefaultAsync();

        public async Task<Trip> SaveTripAsync(TripRequest data, string subjectId)
        {
            var trip = new Trip
            {
                Distance = data.Distance,
                Time = data.Time,
                Started = data.Started,
                Finished = data.Finished,
                UserId = subjectId,
                GroupTripId = data.GroupTripId,
                Route = data.Route
            };

            await _tripsDbSet.AddAsync(trip);
            await _dbContext.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip> RemoveAsync(int tripId, string userId)
        {
            var trip = await GetUserTripAsync(tripId, userId);
            if (trip == null) return null;

            _tripsDbSet.Remove(trip);
            await _dbContext.SaveChangesAsync();
            return trip;
        }

        public async Task<TripPhoto> PutPhotoAsync(Stream photoData, string urlBase, int tripId, string userId)
        {
            var trip = await GetUserTripAsync(tripId, userId);
            if (trip == null) return null;

            Directory.CreateDirectory(UploadsDirectory);
            var fileName = Guid.NewGuid() + ".jpg";
            var filePath = Path.Combine(UploadsDirectory, fileName);

            using (var stream = File.Create(filePath)) await photoData.CopyToAsync(stream);

            var url = $"{urlBase}/{UploadsDirectory}/{fileName}";
            var photo = new TripPhoto {PhotoUrl = url, Trip = trip};
            trip.Photos.Add(photo);
            await _dbContext.SaveChangesAsync();

            return photo;
        }

        public async Task<Trip> FindBySharingId(Guid sharingId) =>
            await _tripsDbSet
                .Where(t => t.SharingGuid == sharingId)
                .Include(t => t.Photos)
                .Include(t => t.Route)
                .Include(t => t.GroupTrip).ThenInclude(gt => gt.Trips).ThenInclude(t => t.Photos)
                .Include(t => t.GroupTrip).ThenInclude(gt => gt.Participants)
                .FirstOrDefaultAsync();

        public async Task<Trip> EnableSharingAsync(int tripId, string userId)
        {
            var trip = await GetUserTripAsync(tripId, userId);
            if (trip == null) return null;

            trip.SharingGuid = Guid.NewGuid();
            await _dbContext.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip> DisableSharingAsync(int tripId, string userId)
        {
            var trip = await GetUserTripAsync(tripId, userId);
            if (trip == null) return null;
            trip.SharingGuid = null;
            await _dbContext.SaveChangesAsync();
            return trip;
        }
    }
}
