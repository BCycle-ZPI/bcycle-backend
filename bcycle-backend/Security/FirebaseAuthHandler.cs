using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bcycle_backend.Security
{
    public class FirebaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string AuthorizationHeaderName = "Authorization";
        private const string BearerTokenPrefix = "Bearer ";

        private readonly FirebaseAuth _auth = FirebaseAuth.DefaultInstance;

        public FirebaseAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!ContainsAuthHeader())
                return AuthenticateResult.Fail("Authentication header not present");

            try
            {
                var token = ExtractToken();
                var decodedToken = await _auth.VerifyIdTokenAsync(token);
                var auth = CreateAuthenticationAsync(decodedToken);
                return AuthenticateResult.Success(auth);
            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail($"Failed to authenticate: {e.Message}");
            }
        }

        private bool ContainsAuthHeader() =>
            Request.Headers.ContainsKey(AuthorizationHeaderName);

        private String ExtractToken()
        {
            string authHeader = Request.Headers[AuthorizationHeaderName];
            return authHeader.Replace(BearerTokenPrefix, "");
        }

        private AuthenticationTicket CreateAuthenticationAsync(FirebaseToken token)
        {
            var claims = new[] {new Claim(ClaimTypes.Sid, token.Uid)};
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationTicket(principal, Scheme.Name);
        }
    }
}
