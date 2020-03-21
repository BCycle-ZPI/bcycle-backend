using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Data;
using bcycle_backend.Models;
using bcycle_backend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace bcycle_backend.Services
{
    public class TripService
    {
        private readonly DbSet<Trip> _tripsDbSet;
        private readonly IConfiguration _configuration;
        private readonly BCycleContext _dbContext;

        public TripService(IConfiguration configuration, BCycleContext context)
        {
            _configuration = configuration;
            _tripsDbSet = context.Trips;
            _dbContext = context;
        }

        public IQueryable<TripDto> GetAll(string userId) =>
            _tripsDbSet
                .Where(t => t.UserId == userId)
                .Include(t => t.TripPhotos)
                .Include(t => t.TripPoints)
                .OrderByDescending(t => t.Started)
                .Select(t => t.AsDto());

        public Task<Trip> GetUserTripAsync(int tripId, string userId) =>
            _tripsDbSet
                .Where(t => t.Id == tripId && t.UserId == userId)
                .Include(t => t.TripPhotos)
                .Include(t => t.TripPoints)
                .FirstOrDefaultAsync();


        public async Task<Trip> SaveTripAsync(TripDto tripDto, string userId)
        {
            var trip = tripDto.AsTrip();
            trip.UserId = userId;

            _tripsDbSet.Add(trip);
            await _dbContext.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip> RemoveAsync(int tripId, string userId)
        {
            var trip = await GetUserTripAsync(tripId, userId);
            return trip == null ? null : await Remove(trip);

            async Task<Trip> Remove(Trip tripToDelete)
            {
                _tripsDbSet.Remove(tripToDelete);
                await _dbContext.SaveChangesAsync();
                return tripToDelete;
            }
        }

        public async Task<TripPhoto> PutPhotoAsync(Stream photoData, String urlBase, int tripId, string userId)
        {
            var trip = await GetUserTripAsync(tripId, userId);

            if (trip == null)
            {
                return null;
            }

            var uploadPath = _configuration.GetValue<string>("UploadPath");
            var uploadPrefix = _configuration.GetValue<string>("UploadPrefix");
            
            Directory.CreateDirectory(uploadPath);
            var fileName =  Guid.NewGuid() + ".jpg";
            var filePath = Path.Combine(uploadPath, fileName);
            
            using (var stream = File.Create(filePath))
                await photoData.CopyToAsync(stream);
            
            var url = $"{urlBase}/{uploadPrefix}/{fileName}";
            var photo = new TripPhoto {PhotoUrl = url, Trip = trip};
            trip.TripPhotos.Add(photo);
            await _dbContext.SaveChangesAsync();
            
            return photo;
        }
    }
}
