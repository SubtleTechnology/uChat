using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Text;
using uChat.Domain;

namespace uChat.Data.Sqlite
{
	public class uChatDataContext : DbContext
	{
        SQLiteConnection conn = new SQLiteConnection("Data Source=uChatDB.sqlite3;Version=3;BinaryGUID=True;");

        public DbSet<Channel> Channels { get; set; }
        public DbSet<User> Users { get; set; }
		public DbSet<Chat> Chats { get; set; }

        //public uChatDataContext()
        //{
        //    Database.EnsureCreated();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@"Data Source=uChatDB.sqlite3;Version=3;BinaryGUID=True;");
            optionsBuilder.UseSqlite(conn);
        }

        public override void Dispose()
        {
            base.Dispose();

            conn.Close();
        }
    }
}
