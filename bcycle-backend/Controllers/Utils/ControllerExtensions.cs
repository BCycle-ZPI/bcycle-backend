using System.Linq;
using Microsoft.AspNetCore.Http;

namespace bcycle_backend.Controllers.Utils
{
    public static class ControllerExtensions
    {
        public static string GetBaseUrl(this HttpRequest request) => $"{request.Scheme}://{request.Host}";
    }
}
