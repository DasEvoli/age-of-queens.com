using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ageofqueenscom.Entities 
{
	[Table("blog_entries")]
	public class BlogEntry
	{
		[Column("entry_id"), Key]
		public int EntryId {get;set;}
		[Column("headline"), Required]
		public string Headline {get;set;}
		[Column("content"), Required]
		public string Content {get;set;}
		[Column("image_url")]
		public string ImageUrl {get;set;}
		[Column("author")]
		public string Author {get;set;}
		[Column("created_at")]
		public DateTime CreatedAt {get;set;}
	}
}