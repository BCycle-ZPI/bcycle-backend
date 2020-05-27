using System;
using System.Threading.Tasks;
using bcycle_backend.Controllers.Utils;
using bcycle_backend.Models;
using bcycle_backend.Models.Responses.Views;
using bcycle_backend.Security;
using bcycle_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bcycle_backend.Controllers
{
    [ApiController]
    [Route("/api/share")]
    public class ShareController : ControllerBase
    {
        private readonly ShareService _shareService;
        private readonly TripService _tripService;

        public ShareController(ShareService shareService, TripService tripService)
        {
            _shareService = shareService;
            _tripService = tripService;
        }

        // GET /api/share/group-trip/:sharingId
        [HttpGet("group-trip/{sharingId:guid}")]
        public async Task<ActionResult<ResultContainer<GroupTripShareView>>> GetGroupTrip(Guid sharingId)
        {
            var groupTripInfo = await _shareService.GetGroupTripAsync(sharingId);
            if (groupTripInfo == null) return NotFound();
            return new ResultContainer<GroupTripShareView>(groupTripInfo);
        }

        // GET /api/share/trip/:sharingId
        [HttpGet("trip/{sharingId:guid}")]
        public async Task<ActionResult<ResultContainer<PrivateTripShareView>>> GetPrivateTrip(Guid sharingId)
        {
            var privateTripInfo = await _shareService.GetPrivateTripAsync(sharingId);
            if (privateTripInfo == null) return NotFound();
            return new ResultContainer<PrivateTripShareView>(privateTripInfo);
        }

        // POST /api/share/:tripId
        [Authorize]
        [HttpPost("{tripId}")]
        public async Task<ActionResult<ResultContainer<string>>> GetSharingUrl(int tripId)
        {
            var trip = await _tripService.EnableSharingAsync(tripId, User.GetId());
            if (trip == null) return BadRequest();

            return new ResultContainer<string>(trip.GetSharingUrl(Request.GetBaseUrl()));
        }

        // DELETE /api/share/:tripId
        [Authorize]
        [HttpDelete("{tripId}")]
        public async Task<IActionResult> DeleteSharingUrl(int tripId) =>
            await _tripService.DisableSharingAsync(tripId, User.GetId()) == null
                ? (IActionResult)NotFound()
                : Ok();
    }
}
