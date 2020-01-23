using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace uChat.Domain
{
	[Table("Mailboxes")]
	public class Mailbox
	{
		public Mailbox()
		{
			MailboxId = Guid.NewGuid().ToString();
		}

		public string MailboxId { get; set; }
		public string UserID { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsSystem { get; set; }

		public List<Mail> Mails { get; } = new List<Mail>();
	}
}
