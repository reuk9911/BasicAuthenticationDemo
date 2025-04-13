using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using BasicAuthenticationDemo.Models.Interfaces;

namespace BasicAuthenticationDemo.Models
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// ссылка на UserService для проверки учетных данных
        /// </summary>
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IUserService userService)
            : base(options, logger, encoder)
        {
            _userService = userService;
        }

        /// <summary>
        /// Метод для обработки аутентификации
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            if (!AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (!"Basic".Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Authorization Scheme");
            }

            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(headerValue.Parameter)).Split(':', 2);

            if (credentials.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid Basic Authentication Credentials");
            }

            var email = credentials[0];
            var password = credentials[1];

            try
            {
                var user = await _userService.ValidateAsync(email, password);
                if (user == null)
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var authenticationTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

                return AuthenticateResult.Success(authenticationTicket);
            }
            catch
            {
                return AuthenticateResult.Fail("Error occurred during authentication");
            }
        }
    }
}