using System.Collections.Generic;
using System.Threading.Tasks;
using bcycle_backend.Models;
using bcycle_backend.Models.Dto;
using bcycle_backend.Security;
using bcycle_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bcycle_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly TripService _tripService;

        public TripsController(TripService tripService)
        {
            _tripService = tripService;
        }

        // GET /api/trips
        [HttpGet]
        public async Task<ActionResult<ResultContainer<IEnumerable<TripDto>>>> Get()
        {
            var trips = await _tripService.GetAll(User.GetId()).ToListAsync();
            return new ResultContainer<IEnumerable<TripDto>>(trips);
        }

        // GET /api/trips/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultContainer<TripDto>>> Get(int id)
        {
            var trip = await _tripService.GetUserTripAsync(id, User.GetId());
            if (trip == null)
            {
                return NotFound();
            }

            return new ResultContainer<TripDto>(trip.AsDto());
        }

        // POST /api/trips
        [HttpPost]
        public async Task<ActionResult<ResultContainer<int>>> Post([FromBody] TripDto tripDto)
        {
            var savedTrip = await _tripService.SaveTripAsync(tripDto, User.GetId());
            return new ResultContainer<int>(savedTrip.Id);
        }

        // PUT /api/trips/{id}/photo
        [HttpPut("{id}/photo")]
        public async Task<ActionResult<ResultContainer<string>>> PutPhoto(int id)
        {
            var photo = await _tripService.PutPhotoAsync(
                Request.Body,
                $"{Request.Scheme}://{Request.Host}",
                id,
                User.GetId()
            );

            if (photo == null)
            {
                return BadRequest();
            }

            return new ResultContainer<string>(photo.PhotoUrl);
        }

        // DELETE /api/trips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) =>
            await _tripService.RemoveAsync(id, User.GetId()) == null
                ? (IActionResult)NotFound()
                : Ok();
    }
}
