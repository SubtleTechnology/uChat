using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uChat.Web.DTO
{
	public class Chat
	{
		public string ChatId;
		public string UserId;
		public User User;
		public DateTime CreatedOn;
		public string Content;
		public string ChannelId;
		public Channel Channel;

		public Chat(Domain.Chat c)
		{
			ChatId = c.ChatId;
			UserId = c.UserId;
			User = new DTO.User(c.User);
			CreatedOn = c.CreatedOn;
			Content = c.Content;
			ChannelId = c.ChannelId;
			Channel = new DTO.Channel(c.Channel);
		}

		public static implicit operator Domain.Chat(DTO.Chat chat)
		{
			var ret = new Domain.Chat()
			{
				ChatId = chat.ChatId,
				UserId = chat.UserId,
				CreatedOn = chat.CreatedOn,
				Content = chat.Content,
				ChannelId = chat.ChannelId
			};
			return ret;
		}
	}
}
