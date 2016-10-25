using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace TrySQLite
{
	public class BloggingContext : DbContext
	{
		public BloggingContext()
		{
		}
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=./blog.db");
		}
	}

    public static class Modelconfiguration 
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasKey(m => m.BlogId);
        }
    }

    public class Blog
	{
		public int BlogId { get; set; }
		public string Url { get; set; }
		public string Name { get; set; }

		public List<Post> Posts { get; set; }
	}

	public class Post
	{
        [PrimaryKey]
        public int PostId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }

		public int BlogId { get; set; }
		public Blog Blog { get; set; }

        [ForeignKey(typeof(Owner))]
        public long? OwnerId { get; set; }
	}

    public class Owner
    {
        public long OwnerId { get; set; }

        public string Name { get; set; }
    }
}

