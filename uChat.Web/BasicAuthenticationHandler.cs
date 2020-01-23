using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using uChat.Domain;
using uChat.Service;

namespace uChat.Web
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public BasicAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
			
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if (!Request.Headers.ContainsKey("Authorization"))
				return AuthenticateResult.Fail("Missing Authorization Header");

			User user = null;
			try
			{
				var mgr = new UserManager();
				var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
				var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' });
				var userId = credentials[0];
				var password = credentials[1];
				string name = Request.Headers["Name"];
				string playfabId = Request.Headers["PlayFabId"];

				user = await mgr.CreateOrUpdateUser(userId, password, name, playfabId);
			}
			catch
			{
				return AuthenticateResult.Fail("Invalid Authorization Header");
			}

			if (user == null)
				return AuthenticateResult.Fail("Invalid Username or Password");

			var claims = new[] {
				new Claim(ClaimTypes.NameIdentifier, user.UserId),
				new Claim(ClaimTypes.Name, user.Name),
			};
			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);

			return AuthenticateResult.Success(ticket);
		}
	}
}

