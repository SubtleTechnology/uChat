using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uChat.Web.DTO
{
	public class Chat
	{
		public Guid ChatId;
		public User User;
		public DateTime CreatedOn;
		public string Content;
		public Guid ChannelId;
		public Channel Channel;

		public Chat(Domain.Chat c)
		{
			ChatId = c.ChatId;
			User = new DTO.User(c.User);
			CreatedOn = c.CreatedOn;
			Content = c.Content;
			ChannelId = c.ChannelId;
			Channel = new DTO.Channel(c.Channel);
		}
	}
}
