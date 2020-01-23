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
	public class ChannelManager
	{
		async public Task<Channel> AddChannel(string name)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				Channel ch = new Channel(name);
				db.Channels.Add(ch);
				await db.SaveChangesAsync();
				return ch;
			}
		}

		async public Task<List<Channel>> GetChannels()
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				return await db.Channels.ToListAsync();
			}
		}

		async public Task<Channel> GetChannel(string channelId)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				channelId = channelId.ToLower();
				return await db.Channels.FindAsync(channelId);
			}
		}

		async public Task<List<Chat>> GetChannelChats(string channelId, DateTime since)
		{
			if (string.IsNullOrEmpty(channelId))
				return null;

			channelId = channelId.ToLower();

			using(uChatDataContext db = new uChatDataContext())
			{
				//string g = "9bb5d62f-2563-4d88-ba43-46fa09b04a1e";
				//var user = await db.Users.FirstOrDefaultAsync(x => x.UserId == g);
				var channel = await db.Channels.FindAsync(channelId);
				var chats = (from c in db.Chats
							 where c.Channel == channel && c.CreatedOn >= since.ToUniversalTime()
							 orderby c.CreatedOn
							 select c)
							 .Include("User")
							 .Include("Channel");

				return await chats.ToListAsync();
			}
		}

		async public Task<Chat> AddChat(Chat chat)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				chat.ChatId = Guid.NewGuid().ToString();
				chat.CreatedOn = chat.CreatedOn.ToUniversalTime();
				db.Chats.Add(chat);
				await db.SaveChangesAsync();
				return chat;
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
