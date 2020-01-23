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
    public class ChannelController : ControllerBase
    {
		readonly ILogger<ChannelController> _logger;
		readonly HttpContext httpContext;

		public ChannelController(IHttpContextAccessor haccess, ILogger<ChannelController> logger)
		{
			httpContext = haccess.HttpContext;
			_logger = logger;
		}

		[HttpGet]
		async public Task<IActionResult> GetAll()
		{
			IEnumerable<DTO.Channel> ret = null;
			try
			{
				var mgr = new ChannelManager();
				var channels = await mgr.GetChannels();
				ret = channels.Select(x => new DTO.Channel(x));
				return Ok(ret);
			}
			catch(Exception ex)
			{
				_logger.LogError($"GetChannelChats: {ex.Message}");
			}
			return StatusCode(500, "Error gettings channel list");
		}

		[HttpPut("{name}")]
		async public Task<IActionResult> AddChannel(string name)
		{
			DTO.Channel ret = null;
			try
			{
				var mgr = new ChannelManager();
				var channel = await mgr.AddChannel(name);
				ret = new DTO.Channel(channel);
				return Ok(ret);
			}
			catch(Exception ex)
			{
				_logger.LogError($"GetChannelChats: {ex.Message}");
			}		
			return StatusCode(500, "Error gettings channel list");
		}

		[HttpGet("{channelId}")]
		async public Task<IActionResult> GetChannel(string channelId)
		{
			return await GetChannel(channelId, SqlDateTime.MinValue.Value);
		}

		[HttpGet("{channelId}/{since}")]
		async public Task<IActionResult> GetChannel(string channelId, DateTime since)
		{
			IEnumerable<DTO.Chat> ret = null;

			try
			{
				var mgr = new ChannelManager();
				var chats = await mgr.GetChannelChats(channelId, since);
				if (chats == null || chats.Count() < 1)
					return NotFound($"Channel Id {channelId} not found");

				ret = chats.Select(x => new DTO.Chat(x));
				return Ok(ret);
			}
			catch(Exception ex)
			{
				_logger.LogError($"GetChannelChats: {ex.Message}");
			}
			return StatusCode(500, "Error getting channel chats");
		}

		[HttpPost("{channelId}")]
		async public Task<IActionResult> AddChat(string channelId, DTO.Chat chat)
		{
			try
			{
				var mgr = new ChannelManager();

				var user = httpContext.User.Identity as ClaimsIdentity;
				var userId = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				var channel = await mgr.GetChannel(channelId);

				chat.ChatId = Guid.NewGuid().ToString();
				chat.UserId = userId;
				chat.ChannelId = channel.ChannelId;
				
				return Ok(await mgr.AddChat((Domain.Chat)chat));
			}
			catch(Exception ex)
			{
				_logger.LogError($"AddChannelChat: {ex.Message}");
			}
			return StatusCode(500, "Error adding chat");
		}
    }
}