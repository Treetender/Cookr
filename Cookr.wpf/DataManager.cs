
using Cookr.lib.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cookr.wpf
{
    class DataManager
    {
        private DbManager db;
        private static readonly Lazy<DataManager> sThis = new Lazy<DataManager>(() => new DataManager());

        public static DataManager Instance => sThis.Value;
        public DbSet<Recipe> Recipes => db.Recipes;
        public DbSet<UnitOfMeasure> UnitOfMeasures => db.UnitOfMeasures;
        public DbContext Database => db;

        private class DbManager : DbContext
        {
            public DbSet<Recipe> Recipes { get; set; }
            public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Filename=./cookr.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Recipe>().HasIndex(r => r.Name).IsUnique();
                modelBuilder.Entity<UnitOfMeasure>().HasIndex(u => u.Name).IsUnique();
            }
        }

        private DataManager()
        {
            db = new DbManager();
            db.Database.EnsureCreated();
        }
    }
}
