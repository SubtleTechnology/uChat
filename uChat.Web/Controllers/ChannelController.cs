using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using uChat.Domain;
using uChat.Service;

namespace uChat.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelController : ControllerBase
    {
		private readonly ILogger<ChannelController> _logger;

		public ChannelController(ILogger<ChannelController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{channelId}")]
		async public Task<IActionResult> GetChannelChats(string channelId)
		{
			return await GetChannelChats(channelId, SqlDateTime.MinValue.Value);
		}

		[HttpGet("{channelId}/{since}")]
		async public Task<IActionResult> GetChannelChats(string channelId, DateTime since)
		{
			IEnumerable<DTO.Chat> ret = null;

			try
			{
				var chatManager = new ChatManager();
				var chats = await chatManager.GetChannelChats(channelId, since);
				ret = chats.Select(x => new DTO.Chat(x));
			}
			catch(Exception ex)
			{
				_logger.LogError("GetChannelChats: " + ex.Message);
			}

			if (ret == null)
				return NotFound();

			return Ok(ret);
		}

		[HttpPut]
		[HttpPost]
		async public Task<IActionResult> AddChannelChat(DTO.Chat chat)
		{
			try
			{
				var chatManager = new ChatManager();
				await chatManager.AddChannelChat((Domain.Chat)chat);
			}
			catch(Exception ex)
			{
				_logger.LogError("AddChannelChat: " + ex.Message);
				return BadRequest();
			}
			return Ok();
		}
    }
}