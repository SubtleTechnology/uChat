using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uChat.Data.Sqlite;
using uChat.Domain;

namespace uChat.Service
{
	public class ChatManager
	{
		async public Task<List<Chat>> GetChannelChats(string channelId, DateTime since)
		{
			if (string.IsNullOrEmpty(channelId))
				return null;

			channelId = channelId.ToLower();

			using(uChatDataContext db = new uChatDataContext())
			{
				//string g = "9bb5d62f-2563-4d88-ba43-46fa09b04a1e";
				//var user = await db.Users.FirstOrDefaultAsync(x => x.UserId == g);
				var channel = await db.Channels.FirstOrDefaultAsync(x => x.ChannelId == channelId);
				var chats = (from c in db.Chats
							 where c.Channel == channel && c.CreatedOn >= since.ToUniversalTime()
							 orderby c.CreatedOn
							 select c)
							 .Include("User")
							 .Include("Channel");

				return await chats.ToListAsync();
			}
		}

		async public Task AddChannelChat(Chat chat)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				chat.CreatedOn = chat.CreatedOn.ToUniversalTime();
				db.Chats.Add(chat);
				await db.SaveChangesAsync();
			}			
		}

		async public Task<Chat> GetChat(string id)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				var chat = await db.Chats.FindAsync(id);
				return chat;
			}			
		}
	}
}
