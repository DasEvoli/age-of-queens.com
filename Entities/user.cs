using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ageofqueenscom.Entities 
{
	[Table("users")]
	public class User
	{
		[Column("user_id"), Key]
		public int UserId {get;set;}
		[Column("user_name"), Required]
		public string UserName {get;set;}
		[Column("user_password"), Required]
		public string UserPassword {get;set;}
		[Column("last_login")]
		public DateTime? LastLogin {get;set;}
		[Column("session")]
		public string Session {get;set;}
	}
}