using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
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
    public class UserController : ControllerBase
    {
		private readonly ILogger<UserController> _logger;

		public UserController(ILogger<UserController> logger)
		{
			_logger = logger;
		}

		//[Authorize]
  //      public async Task<IAsyncResult> CreateOrUpdateUser(DTO.User user)
  //      {
		//	try
		//	{
		//		var mgr = new ChannelManager();

		//		var user = httpContext.User.Identity as ClaimsIdentity;
		//		var userId = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
		//		var channel = await mgr.GetChannel(channelId);

		//		chat.ChatId = Guid.NewGuid().ToString();
		//		chat.UserId = userId;
		//		chat.ChannelId = channel.ChannelId;
				
		//		return Ok(await mgr.AddChat((Domain.Chat)chat));
		//	}
		//	catch(Exception ex)
		//	{
		//		_logger.LogError($"AddChannelChat: {ex.Message}");
		//	}
		//	return StatusCode(500, "Error adding chat");
  //      }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromForm]DTO.AuthRequest userParam)
        {
			var user = await new UserManager().Authenticate(userParam.UserId, userParam.Password, userParam.PlayFabId);
            if (user == null)
                return BadRequest(new { message = "UserId or Password is incorrect" });

            return Ok(user);
        }
    }
}