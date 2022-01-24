using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Context
{
    public class RSSContextEF:DbContext
    {
        public DbSet<SourceContentImage> Images { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SourceContent> SourceContents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<EmailAccountOptions> EmailAccountOptions { get; set; }
        public DbSet<EmailOptions> EmailOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=TCBLGADMCONS023;Initial Catalog=InstaAutoPostRssDb;Integrated Security=SSPI;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
   
}
