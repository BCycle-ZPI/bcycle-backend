using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Data;
using bcycle_backend.Models;
using bcycle_backend.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace bcycle_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BCycleContext _dbContext;

        public TripsController(IConfiguration configuration, BCycleContext dbContext) : base()
        {
            this._configuration = configuration;
            this._dbContext = dbContext;
        }


        // temporary testing placeholder
        [HttpPost("fakeuser")]
        public async Task<ActionResult> CreateFakeUser()
        {
            User user = await UserDataHelper.GetCurrentUserDetails();
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        // GET api/trips
        [HttpGet]
        public async Task<ActionResult<ResultContainer<IEnumerable<TripDto>>>> Get()
        {
            int userID = (await UserDataHelper.GetCurrentUserID()).Value;
            return new ResultContainer<IEnumerable<TripDto>>(
                await _dbContext.Trips
                    .Where(t => t.UserID == userID)
                    .Include(t => t.TripPhotos).Include(t => t.TripPoints)
                    .OrderByDescending(t => t.Started)
                    .Select(t => t.AsDto())
                    .ToListAsync());
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultContainer<TripDto>>> Get(int id)
        {
            return new ResultContainer<TripDto>((await _dbContext.GetMyTrip(id)).AsDto());
        }

        // POST api/trips
        [HttpPost]
        public async Task<ActionResult<ResultContainer<int>>> Post([FromBody] TripDto tripDto)
        {
            tripDto.ID = 0;
            Trip trip = tripDto.AsTrip();
            trip.UserID = (await UserDataHelper.GetCurrentUserID()).Value;
            
            _dbContext.Add(trip);
            await _dbContext.SaveChangesAsync();
            return new ResultContainer<int>(trip.ID);
        }

        // PUT api/trips/5/photo
        [HttpPut("{id}/photo")]
        public async Task<ActionResult<ResultContainer<string>>> PutPhoto(int id)
        {
            Trip trip = await _dbContext.GetMyTrip(id);

            string uploadPath = _configuration.GetValue<string>("UploadPath");
            string uploadPrefix = _configuration.GetValue<string>("UploadPrefix");
            Directory.CreateDirectory(uploadPath);

            Guid guid = Guid.NewGuid();
            string fileName = guid.ToString() + ".jpg";
            string filePath = Path.Combine(uploadPath, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await Request.Body.CopyToAsync(stream);
            }

            string url = $"{Request.Scheme}://{Request.Host}/{uploadPrefix}/{fileName}";
            trip.TripPhotos.Add(
                new TripPhoto { PhotoUrl = url, Trip = trip }
            );

            await _dbContext.SaveChangesAsync();
            return new ResultContainer<string>(url);
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Trip trip = await _dbContext.GetMyTrip(id);
            _dbContext.Remove(trip);
            return Ok();
        }
    }
}
