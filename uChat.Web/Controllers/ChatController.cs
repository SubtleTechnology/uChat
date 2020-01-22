﻿using System;
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
		public List<DTO.Chat> GetChannel(Guid channelId)
		{
			var chatManager = new ChatManager();
			var chats = chatManager.GetChannelSince(channelId, SqlDateTime.MinValue.Value);
			var ret = (from c in chats select new DTO.Chat(c)).ToList();
			return ret;
		}

		[HttpGet("{channelId}/{since}")]
		public List<DTO.Chat> GetChannelSince(Guid channelId, DateTime since)
		{
			var chatManager = new ChatManager();
			var chats = chatManager.GetChannelSince(channelId, since);
			return (from c in chats select new DTO.Chat(c)).ToList();
		}
    }
}