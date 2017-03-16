using Core.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.data.DB
{
    public abstract class Db : DbContext
    {
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<UnitOfMeasure>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }
    };
}
