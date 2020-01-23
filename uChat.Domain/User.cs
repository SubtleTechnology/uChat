using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace uChat.Domain
{
	[Table("Users")]
	public class User
	{
		public bool IsAdmin { get; set; }
		public string UserId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Name { get; set; }
		public byte[] Password { get; set; }
		public string PlayFabId { get; set; }
	}
}
