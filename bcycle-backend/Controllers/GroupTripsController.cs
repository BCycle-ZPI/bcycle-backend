using System.Threading.Tasks;
using bcycle_backend.Models;
using bcycle_backend.Models.Entities;
using bcycle_backend.Models.Requests;
using bcycle_backend.Security;
using bcycle_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bcycle_backend.Controllers
{
    [Route("/api/group-trips")]
    [ApiController]
    [Authorize]
    public class GroupTripsController : ControllerBase
    {
        private readonly GroupTripService _tripService;

        public GroupTripsController(GroupTripService tripService)
        {
            _tripService = tripService;
        }

        // POST /api/group-trips
        [HttpPost]
        public async Task<ActionResult<ResultContainer<int>>> Create([FromBody] GroupTripRequest data)
        {
            var trip = await _tripService.CreateAsync(data, User.GetId());
            return new ResultContainer<int>(trip.Id);
        }

        // TODO: return user data instead of ids
        // GET /api/group-trips/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultContainer<GroupTrip>>> Get(int id)
        {
            var trip = await _tripService.FindAsync(id, User.GetId());
            if (trip == null) return NotFound();

            return new ResultContainer<GroupTrip>(trip);
        }

        // PUT /api/group-trips/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GroupTripRequest data)
        {
            var result = await _tripService.UpdateAsync(id, data, User.GetId());
            return CreateResponse(result);
        }

        // DELETE /api/group-trips/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tripService.RemoveAsync(id, User.GetId());
            return CreateResponse(result);
        }

        // POST /api/group-trips/join/:code
        [HttpPost("join/{code}")]
        public async Task<ActionResult<ResultContainer<int>>> Join(string code)
        {
            var trip = await _tripService.Join(User.GetId(), code);
            if (trip == null) return NotFound();

            return new ResultContainer<int>(trip.Id);
        }

        // POST /group-trips/:tripId/requests/:userid
        [HttpPost("{tripId}/requests/{userId}")]
        public async Task<IActionResult> Accept(int tripId, string userId)
        {
            var result = await _tripService.AcceptRequestAsync(tripId, userId, User.GetId());
            return CreateResponse(result);
        }
       
        // DELETE /group-trips/:tripId/requests/:userid
        [HttpDelete("{tripId}/requests/{userId}")]
        public async Task<IActionResult> Reject(int tripId, string userId)
        {
            var result = await _tripService.RejectRequestAsync(tripId, userId, User.GetId());
            return CreateResponse(result);
            
        }
        
        // DELETE /group-trips/:tripId/participants/:userid
        [HttpDelete("{tripId}/participants/{userId}")]
        public async Task<IActionResult> RemoveParticipant(int tripId, string userId)
        {
            var result = await _tripService.RemoveParticipant(tripId, userId, User.GetId());
            return CreateResponse(result);
        }
        
        // We may choose better way for returning responses rather than simple null/non null if we need to 
        private IActionResult CreateResponse(object obj) => obj == null ? (IActionResult)NotFound() : Ok();
    }
}
