using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using uChat.Domain;
using uChat.Service;

namespace uChat.Web.Controllers
{
	[Authorize]
    [ApiController]
    [Route("[controller]")]
	public class MailController : ControllerBase
	{
		readonly ILogger<MailController> _logger;
		readonly HttpContext httpContext;

		public MailController(IHttpContextAccessor haccess, ILogger<MailController> logger)
		{
			httpContext = haccess.HttpContext;
			_logger = logger;
		}

		[HttpGet("{since}")]
		async public Task<IActionResult> GetMail(DateTime since)
		{
			try
			{		
				var user = httpContext.User.Identity as ClaimsIdentity;
				var userId = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				var mgr = new MailManager();
				return Ok(await mgr.GetMail(userId, since));
			}
			catch(Exception ex)
			{
				_logger.LogError($"GetChannelChats: {ex.Message}");
			}
			return StatusCode(500, $"Error getting mail since {since}");
		}

		[HttpPost]
		async public Task<IActionResult> AddMail(Mail mail)
		{
			try
			{ 
				var user = httpContext.User.Identity as ClaimsIdentity;
				var userId = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				var mgr = new MailManager();
				return Ok(await mgr.AddMail(mail));
			}
			catch(Exception ex)
			{
				_logger.LogError($"GetChannelChats: {ex.Message}");
			}
			return StatusCode(500, "Error adding mail");
		}
	}
}
