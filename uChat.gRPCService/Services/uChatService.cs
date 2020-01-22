using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace uChat.gRPCService
{
	public class uChatService : uChat.uChatBase
	{
		readonly ILogger<uChatService> _logger;

		public uChatService(ILogger<uChatService> logger)
		{
			_logger = logger;
		}

		public override Task<ChatReply> Say(ChatRequest request, ServerCallContext context)
		{
			return Task.FromResult(new ChatReply
			{
				Message = "OK"
			});
		}
	}
}
