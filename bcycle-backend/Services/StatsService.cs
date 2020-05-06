using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Data;
using bcycle_backend.Models.Entities;
using bcycle_backend.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace bcycle_backend.Services
{
    public class StatsService
    {
        private readonly DbSet<Trip> _tripsDbSet;
        private readonly DbSet<GroupTrip> _groupTripsDbSet;
        private readonly DbSet<GroupTripParticipant> _groupTripParticipantsDbSet;
        private readonly BCycleContext _dbContext;
        private const double SECONDS_PER_MINUTE = 60.0;

        public StatsService(BCycleContext context)
        {
            _tripsDbSet = context.Trips;
            _groupTripsDbSet = context.GroupTrips;
            _groupTripParticipantsDbSet = context.GroupTripParticipants;
            _dbContext = context;
        }

        public async Task<UserStats> GetUserStats(string userId) =>
            new UserStats
            {
                TripCount = await GetUserTripCount(userId),
                GroupTripTotalCount = await GetUserGroupTripTotalCount(userId),
                GroupTripHostingCount = await GetUserGroupTripHostingCount(userId),
                TotalKilometers = await GetTripsTotalKilometers(userId),
                TotalTimeMinutes = await GetTripsTotalTimeMinutes(userId)
            };

        private async Task<int> GetUserTripCount(string userId) =>
            await _tripsDbSet
                .Where(t => t.UserId == userId)
                .CountAsync();


        private async Task<int> GetUserGroupTripHostingCount(string userId) =>
            await _groupTripsDbSet
                .Where(gt => gt.HostId == userId)
                .CountAsync();

        private async Task<int> GetUserGroupTripTotalCount(string userId) =>
            (await GetUserGroupTripHostingCount(userId)) +
            (await _groupTripParticipantsDbSet
                .Where(gtp => gtp.UserId == userId)
                .Where(gtp => gtp.Status == ParticipantStatus.Accepted)
                .Include(gtp => gtp.GroupTrip)
                .Where(gtp => gtp.GroupTrip.HostId != userId)
                .CountAsync());

        private async Task<double> GetTripsTotalKilometers(string userId) =>
            await _tripsDbSet
                .Where(t => t.UserId == userId)
                .SumAsync(t => t.Distance);

        private async Task<double> GetTripsTotalTimeMinutes(string userId) =>
            await _tripsDbSet
                .Where(t => t.UserId == userId)
                .SumAsync(t => t.Time) / SECONDS_PER_MINUTE;

    }
}
