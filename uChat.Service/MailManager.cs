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
	public class MailManager
	{
		async public Task<List<Mail>> GetMail(string userId, DateTime since)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				userId = userId.ToLower();

				var mb = await db.Mailboxes.FindAsync(userId);
				var mails = db.Mails.Where(x => x.ToUserId == userId && x.CreatedOn >= since);
				return await mails.ToListAsync();
			}
		}

		async public Task<Mail> AddMail(Mail mail)
		{
			using(uChatDataContext db = new uChatDataContext())
			{
				var mb = await db.Mailboxes.FindAsync(mail.FromUserId);
				mail.MailId = Guid.NewGuid().ToString().ToLower();
				mail.CreatedOn = DateTime.UtcNow;
				mail.Mailbox = mb;
				mail.MailboxId = mb.MailboxId;
				db.Mails.Add(mail);
				return mail;
			}
		}
	}
}
