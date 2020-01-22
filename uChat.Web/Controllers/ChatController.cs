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
    public class ChatController : ControllerBase
    {
		private readonly ILogger<ChatController> _logger;

		public ChatController(ILogger<ChatController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{channelId}")]
		async public Task<IActionResult> Get(string chatId)
		{
			DTO.Chat ret = null;

			try
			{
				var chatManager = new ChatManager();
				var chat = await chatManager.GetChat(chatId);
				if (chat != null)
					ret = new DTO.Chat(chat);
			}
			catch(Exception ex)
			{
				_logger.LogError("Chat - Get:" + ex.Message);
			}

			if (ret == null)
				return NotFound();

			return Ok(ret);
		}
    }
}