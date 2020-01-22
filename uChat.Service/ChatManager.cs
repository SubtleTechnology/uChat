using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uChat.Data.Sqlite;
using uChat.Domain;

namespace uChat.Service
{
	public class ChatManager
	{
		public IEnumerable<Chat> GetChannelSince(Guid channelId, DateTime since)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				var chats = (from c in db.Chats 
							 //where c.ChannelId == channelId// && c.CreatedOn >= since 
							 orderby c.CreatedOn
							 select c);
				return chats.ToList();
			}
		}

		public void Add(Chat chat)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				db.Chats.Add(chat);
				db.SaveChanges();
			}			
		}
	}
}
