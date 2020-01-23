using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace uChat.Domain
{
	[Table("Channels")]
	public class Channel
	{
		public string ChannelId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Name { get; set; }
		public bool IsSystem { get; set; }

		public List<Chat> Chats { get; } = new List<Chat>();

		public Channel()
		{
			ChannelId = Guid.NewGuid().ToString();
		}

		public Channel(string name)
		{
			ChannelId = Guid.NewGuid().ToString();
			Name = name;
		}
	}
}
