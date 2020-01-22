using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uChat.Web.DTO
{
	public class Channel
	{
		public string ChannelId;
		public string Name;

		public Channel(Domain.Channel c)
		{
			ChannelId = c.ChannelId;
			Name = c.Name;
		}
	}
}
