using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ageofqueenscom.Entities 
{
	[Table("mod_entries")]
	public class ModEntry
	{
		[Column("mod_id"), Key]
		public int ModId {get;set;}
		[Column("name"), Required]
		public string Name {get;set;}
		[Column("description")]
		public string Description {get;set;}
		[Column("creator")]
		public string Creator {get;set;}
		[Column("upload_date")]
		public DateTime UploadDate {get;set;}
		[Column("category")]
		public string Category {get;set;}
		[Column("image_url")]
		public string ImageUrl {get;set;}
	}
}