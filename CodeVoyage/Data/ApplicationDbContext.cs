﻿using CodeVoyage.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeVoyage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost>  BlogPosts { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<BlogImage>  BlogImages { get; set; }
    }
}
