using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ageofqueenscom.Entities 
{
	[Table("introduction_entries")]
	public class IntroductionEntry
	{
		[Column("entry_id"), Key]
		public int EntryId {get;set;}
		[Column("name"), Required]
		public string Name {get;set;}
		[Column("content")]
		public string Content {get;set;}
		[Column("image_url")]
		public string ImageUrl {get;set;}
		[Column("twitter_url")]
		public string TwitterUrl {get;set;}
		[Column("youtube_url")]
		public string YoutubeUrl {get;set;}
		[Column("twitch_url")]
		public string TwitchUrl {get;set;}
		[Column("instagram_url")]
		public string InstagramUrl {get;set;}
	}
}