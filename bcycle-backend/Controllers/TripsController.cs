using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Controllers.Utils;
using bcycle_backend.Models;
using bcycle_backend.Models.Entities;
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
            var trips = await _tripService.GetAll(User.GetId()).ToListAsync();
            return new ResultContainer<IEnumerable<TripResponse>>(trips.Select(AsResponse));
        }

        // GET /api/trips/:id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResultContainer<TripResponse>>> Get(int id)
        {
            var trip = await _tripService.GetUserTripAsync(id, User.GetId());
            if (trip == null) return NotFound();

            return new ResultContainer<TripResponse>(AsResponse(trip));
        }

        // POST /api/trips
        [HttpPost]
        public async Task<ActionResult<ResultContainer<int>>> Post([FromBody] TripRequest data)
        {
            var savedTrip = await _tripService.SaveTripAsync(data, User.GetId());
            return new ResultContainer<int>(savedTrip.Id);
        }

        // PUT /api/trips/:id/photo
        [HttpPut("{id}/photo")]
        public async Task<ActionResult<ResultContainer<string>>> PutPhoto(int id)
        {
            var photo = await _tripService.PutPhotoAsync(
                Request.Body,
                Request.GetBaseUrl(),
                id,
                User.GetId()
            );

            if (photo == null) return BadRequest();

            return new ResultContainer<string>(photo.PhotoUrl);
        }

        // DELETE /api/trips/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) =>
            await _tripService.RemoveAsync(id, User.GetId()) == null
                ? (IActionResult)NotFound()
                : Ok();

        private TripResponse AsResponse(Trip trip) => trip.AsResponse(Request.GetBaseUrl());
    }
}
