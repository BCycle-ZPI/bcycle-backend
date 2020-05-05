using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Models;
using bcycle_backend.Models.Requests;
using bcycle_backend.Models.Responses;
using bcycle_backend.Security;
using bcycle_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bcycle_backend.Controllers
{
    [Route("/api/trips")]
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
        public async Task<ActionResult<ResultContainer<IEnumerable<TripResponse>>>> Get()
        {
            var trips = await _tripService.GetAll(User.GetId()).Select(t => t.AsResponse()).ToListAsync();
            return new ResultContainer<IEnumerable<TripResponse>>(trips);
        }

        // GET /api/trips/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResultContainer<TripResponse>>> Get(int id)
        {
            var trip = await _tripService.GetUserTripAsync(id, User.GetId());
            if (trip == null) return NotFound();

            return new ResultContainer<TripResponse>(trip.AsResponse());
        }

        // GET /api/trips/{guid}
        [HttpGet("{guid:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultContainer<TripResponse>>> GetPublic(Guid guid)
        {
            var trip = await _tripService.GetPublicTripAsync(guid);
            if (trip == null) return NotFound();

            return new ResultContainer<TripResponse>(trip.AsResponse());
        }


        // POST /api/trips
        [HttpPost]
        public async Task<ActionResult<ResultContainer<int>>> Post([FromBody] TripRequest data)
        {
            var savedTrip = await _tripService.SaveTripAsync(data, User.GetId());
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

            if (photo == null) return BadRequest();

            return new ResultContainer<string>(photo.PhotoUrl);
        }

        // DELETE /api/trips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) =>
            await _tripService.RemoveAsync(id, User.GetId()) == null
                ? (IActionResult)NotFound()
                : Ok();

        // POST /api/trips/{id}/share
        [HttpPost("{id}/share")]
        public async Task<ActionResult<ResultContainer<string>>> GetSharingUrl(int id)
        {
            var sharingUrl = await _tripService.EnableSharingAsync($"{Request.Scheme}://{Request.Host}", id, User.GetId());
            if (sharingUrl == null) return BadRequest();

            return new ResultContainer<string>(sharingUrl);
        }

        // DELETE /api/trips/{id}/share
        [HttpDelete("{id}/share")]
        public async Task<IActionResult> DeleteSharingUrl(int id) =>
            await _tripService.DisableSharingAsync(id, User.GetId()) == null
                ? (IActionResult)NotFound()
                : Ok();

    }
}
