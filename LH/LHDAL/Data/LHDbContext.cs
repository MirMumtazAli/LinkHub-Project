using LHDAL.DAos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHDAL.Data
{
    public class LHDbContext : IdentityDbContext
    {
        public LHDbContext(DbContextOptions<LHDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        //production server
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"workstation id=linkhubdb.mssql.somee.com;packet size=4096;user id=MirMumtazAli_SQLLogin_1;pwd=25d86fez1o;data source=linkhubdb.mssql.somee.com;persist security info=False;initial catalog=linkhubdb;TrustServerCertificate=True");
        //}

        //development server
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-3UCO5LS\SQLEXPRESS;Initial Catalog=LhDb_live;Integrated Security=True;TrustServerCertificate=True");
        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<LHUser> LHUsers { get; set; }
        public DbSet<LHUrl> LHUrls { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Technology", CategoryDescription = "All things tech-related" },
                new Category { CategoryId = 2, CategoryName = "Health", CategoryDescription = "Helath and wellness" },
                new Category { CategoryId = 3, CategoryName = "Finance", CategoryDescription = "Finance news and tips" },
                new Category { CategoryId = 4, CategoryName = "Education", CategoryDescription = "Educational content" },
                new Category { CategoryId = 5, CategoryName = "Travel", CategoryDescription = "Travel guides and tips" },
                new Category { CategoryId = 6, CategoryName = "Food", CategoryDescription = "Recipes and food reviews" },
                new Category { CategoryId = 7, CategoryName = "Fashion", CategoryDescription = "Latest fashion trends" },
                new Category { CategoryId = 8, CategoryName = "Sports", CategoryDescription = "Sports coverage and analysis" },
                new Category { CategoryId = 9, CategoryName = "Entertainment", CategoryDescription = "Movies, Tv Shouws" },
                new Category { CategoryId = 10, CategoryName = "Science", CategoryDescription = "Scientific Discovery" }
                );

            builder.Entity<LHUser>(entity =>
            {
                entity.ToTable("LHUser");
                entity.Property(e => e.AlternateContact).HasMaxLength(50);
            });

            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(x => x.CategoryId);
                entity.Property(e => e.CategoryName).HasMaxLength(50);
                entity.Property(e => e.CategoryDescription).HasMaxLength(500);
            });

            builder.Entity<LHUrl>().ToTable("LHUrl");
            builder.Entity<LHUrl>().HasKey(x => x.UrlId);
            builder.Entity<LHUrl>().Property(x => x.IsApproved);


            builder.Entity<LHUrl>()
                            .HasOne(c => c.Category)
                            .WithMany(url => url.LHUrls)
                            .HasForeignKey(x => x.CategoryId);

            builder.Entity<LHUrl>()
                            .HasOne(c => c.User)
                            .WithMany(url => url.LHUrls)
                            .HasForeignKey(x => x.Id);


        }
    }
}
