using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uChat.Web.DTO
{
	public class Chat
	{
		public Guid ChatId { get; set; }
		public User User { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Content { get; set; }
		public Guid ChannelId { get; set; }

		public Chat(Domain.Chat c)
		{
			ChatId = c.ChatId;
			User = new DTO.User(c.User);
			CreatedOn = c.CreatedOn;
			Content = c.Content;
			ChannelId = c.ChannelId;
		}
	}
}
