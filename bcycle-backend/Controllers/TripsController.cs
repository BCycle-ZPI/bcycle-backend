using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Data;
using bcycle_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace bcycle_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        public TripsController(IConfiguration configuration) : base()
        {
            this.Configuration = configuration;
        }
        // GET api/trips
        [HttpGet]
        public async Task<ActionResult<ResultContainer<IEnumerable<Trip>>>> Get()
        {
            int userID = (await UserDataHelper.GetCurrentUserID()).Value;
            using (var db = new BCycleContext())
            {
                return new ResultContainer<IEnumerable<Trip>>(
                    await db.Trips
                        .Where(t => t.UserID == userID)
                        .OrderByDescending(t => t.Started)
                        .ToListAsync());
            }
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultContainer<Trip>>> Get(int id)
        {
            using (var db = new BCycleContext())
            {
                return new ResultContainer<Trip>(await db.GetMyTrip(id));
            }
        }

        // POST api/trips
        [HttpPost]
        public async Task<ActionResult<ResultContainer<int>>> Post([FromBody] Trip trip)
        {
            trip.User = null;
            trip.UserID = (await UserDataHelper.GetCurrentUserID()).Value;
            
            using (var db = new BCycleContext())
            {
                db.Add(trip);
                int results = await db.SaveChangesAsync();
                return new ResultContainer<int>(trip.ID);
            }
        }

        // PUT api/trips/5/photo
        [HttpPut("{id}/photo")]
        public async Task<ActionResult<ResultContainer<string>>> PutPhoto(int id)
        {
            using (var db = new BCycleContext())
            {
                Trip trip = await db.GetMyTrip(id);
                string uploadPath = Configuration.GetValue<string>("UploadPath");
                string uploadPrefix = Configuration.GetValue<string>("UploadPrefix");
                Guid guid = new Guid();
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

                await db.SaveChangesAsync();
                return new ResultContainer<string>(url);
            }
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            using (var db = new BCycleContext())
            {
                Trip trip = await db.GetMyTrip(id);
                db.Remove(trip);
            }
            return Ok();
        }
    }
}
