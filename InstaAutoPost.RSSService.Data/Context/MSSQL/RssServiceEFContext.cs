using InstaAutoPost.RSSService.Data.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Context.MSSQL
{
    public class RssServiceEFContext:DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SourceContent> SourceContents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=TCBLGADMCONS023;Initial Catalog=InstaAutoPostRssDb;Integrated Security=SSPI;");
        }
    }
}
