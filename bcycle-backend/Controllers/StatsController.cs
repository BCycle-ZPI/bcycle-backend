using System.Threading.Tasks;
using bcycle_backend.Models;
using bcycle_backend.Models.Responses;
using bcycle_backend.Security;
using bcycle_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bcycle_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/stats")]
    public class StatsController : ControllerBase
    {
        private readonly StatsService _statsService;

        public StatsController(StatsService statsService)
        {
            _statsService = statsService;
        }

        // GET /api/stats
        [HttpGet]
        public async Task<ActionResult<ResultContainer<UserStats>>> GetUserStats()
        {
            var stats = await _statsService.GetUserStats(User.GetId());
            return new ResultContainer<UserStats>(stats);
        }
    }
}
