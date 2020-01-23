using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace uChat.Domain
{
	[Table("Mails")]
	public class Mail
	{
		public Mail()
		{
			MailId = Guid.NewGuid().ToString();
		}

		public string MailboxId { get; set; }
		public Mailbox Mailbox { get; set; }
		public string MailId { get; set; }
		public string FromUserId { get; set; }
		public string ToUserId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Content { get; set; }
	}
}
