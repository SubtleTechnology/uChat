using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uChat.Web.DTO
{
	public class User
	{
		public string UserId;
		public string Name;

		public User(Domain.User u)
		{
			UserId = u.UserId;
			Name = u.Name;
		}
	}
}
