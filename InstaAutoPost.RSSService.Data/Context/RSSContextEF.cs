using InstaAutoPost.RSSService.Data.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Proxies;


namespace InstaAutoPost.RSSService.Data.Context
{
    public class RSSContextEF:DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SourceContent> SourceContents { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"Data Source=TCBLGADMCONS023;Initial Catalog=InstaAutoPostRssDb;Integrated Security=SSPI;");
        }
    }
}
