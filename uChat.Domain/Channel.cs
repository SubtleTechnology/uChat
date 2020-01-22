using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace uChat.Domain
{
	[Table("Channels")]
	public class Channel
	{
		public Guid ChannelId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Name { get; set; }

		public List<Chat> Chats { get; } = new List<Chat>();
	}
}
