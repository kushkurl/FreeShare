using FreeShare.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeShare.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AuthenticationFreeShareDB;Integrated Security=true;");

            //builder.UseSqlite(@"Data Source=c:\Temp\mydb/db");
        }

        // In order to add any model to the database, we need an entry here
        public DbSet<Models.Data> Data { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryType> CategoryType { get; set; }


        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Category.Any() || context.CategoryType.Any())
            {
                return;   // DB has been seeded
            }

            var categoryType = new CategoryType[]
           {
                new CategoryType { Name = "Cover Letter" },
                new CategoryType { Name = "Resume" }
           };
            var allcType = from c in context.CategoryType select c;
            context.CategoryType.RemoveRange(allcType);
            context.SaveChanges();

            foreach (CategoryType d in categoryType)
            {
                context.CategoryType.Add(d);
            }
            context.SaveChanges();


            var category = new Category[]
           {
                new Category { NameToken = "Action and Adventure",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Cover Letter"),
                    Description = "Action and Adventure" },
                new Category { NameToken = "Crime",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Cover Letter"),
                    Description = "Crime" },
                new Category { NameToken = "Drama",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Cover Letter"),
                    Description = "Drama" },
                new Category { NameToken = "Dictionary",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Resume"),
                    Description = "Dictionary" },
                new Category { NameToken = "Humor",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Resume"),
                    Description = "Humor" }
           };
            var allcategory = from c in context.Category select c;
            context.Category.RemoveRange(allcategory);
            context.SaveChanges();

            foreach (Category i in category)
            {
                context.Category.Add(i);
            }
            context.SaveChanges();
        }
        public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                builder.Property(u => u.FirstName).HasMaxLength(255);
                builder.Property(u => u.LastName).HasMaxLength(255);
            }
        }

    }
}
