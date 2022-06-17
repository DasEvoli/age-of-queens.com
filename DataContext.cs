using Microsoft.EntityFrameworkCore;
using Ageofqueenscom.Entities;

namespace Ageofqueenscom.Data
{
	public class DataContext : DbContext
	{
		public string DbPath { get; }

		public DataContext(DbContextOptions<DataContext> options) : base(options){}

		public DbSet<User> Users {get;set;}
		public DbSet<BlogEntry> BlogEntries {get;set;}
		public DbSet<IntroductionEntry> IntroductionEntries {get;set;}
		public DbSet<ModEntry> ModEntries {get;set;}
	}
}