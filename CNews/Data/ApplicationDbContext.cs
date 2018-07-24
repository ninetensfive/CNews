using System;
using System.Collections.Generic;
using System.Text;
using CNews.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CNews.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasOne(l => l.Author).WithMany(u => u.AuthorArticles).HasForeignKey(u => u.AuthorId);
            modelBuilder.Entity<Article>().HasOne(l => l.Editor).WithMany(u => u.EditorArticles).HasForeignKey(u => u.EditorId);
            modelBuilder.Entity<Article>().HasOne(l => l.Designer).WithMany(u => u.DesignerArticles).HasForeignKey(u => u.DesignerId);
        }
    }
}
