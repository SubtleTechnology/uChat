using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace uChat.Domain
{
	[Table("Chats")]
	public class Chat
	{
		public string ChatId { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Content { get; set; }
		public string ChannelId { get; set; }
		public Channel Channel { get; set; }
	}
}
