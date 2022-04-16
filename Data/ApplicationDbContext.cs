using FreeShare.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DataListRazorDB;Integrated Security=true;");

            //builder.UseSqlite(@"Data Source=c:\Temp\mydb/db");
        }

        // In order to add any model to the database, we need an entry here
        public DbSet<Models.Data> Book { get; set; }
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
                new CategoryType { Name = "Fictional" },
                new CategoryType { Name = "Non-Fictional" }
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
                new Category { NameToken = "Action and Adventure",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Fictional"),
                    Description = "Action and Adventure" },
                new Category { NameToken = "Crime",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Fictional"),
                    Description = "Crime" },
                new Category { NameToken = "Drama",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Fictional"),
                    Description = "Drama" },
                new Category { NameToken = "Dictionary",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Non-Fictional"),
                    Description = "Dictionary" },
                new Category { NameToken = "Humor",     TypeId = context.CategoryType.FirstOrDefault(b => b.Name == "Non-Fictional"),
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

        }
    }
