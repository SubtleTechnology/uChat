using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using uChat.Data.Sqlite;
using uChat.Domain;

namespace uChat.Service
{
	public class UserManager
	{
		async public Task<User> CreateOrUpdateUser(string userId, string password, string name, string playFabId)
		{
			if (password.Length > 20)
				password = password.Left(20);

			Domain.User user = null;
			using(var db = new uChatDataContext())
			{
				if (!string.IsNullOrEmpty(userId))
					user = await db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
				if (user == null)
				{
					user = new Domain.User();
					user.CreatedOn = DateTime.UtcNow;
					user.UserId = userId;
					user.Name = name;
					user.UserId = Guid.NewGuid().ToString();
					PasswordHash hash = new PasswordHash(password);
					byte[] hashBytes = hash.ToArray();
					user.Password = hashBytes;
					user.PlayFabId = playFabId;

					db.Users.Add(user);
					await db.SaveChangesAsync();
				}
				else
				{
					byte[] hashBytes = user.Password; //read from store.
					PasswordHash hash = new PasswordHash(hashBytes);
					if (!hash.Verify(password))
						return null;
				}

				return user;
			}
		}

		async public Task<User> Authenticate(string userId, string password, string playFabId)
		{
			User user = null;
			using(var db = new uChatDataContext())
			{
				if (!string.IsNullOrEmpty(playFabId))
					user = await db.Users.FirstOrDefaultAsync(x => x.PlayFabId == playFabId);
				else
					user = await db.Users.FirstOrDefaultAsync(x => x.UserId == userId);

				return user;
			}
		}
	}
}
