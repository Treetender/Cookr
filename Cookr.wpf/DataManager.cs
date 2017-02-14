using Core.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Cookr.wpf
{
    class DataManager : IDisposable
    {
        private DbManager db;
        private ObservableCollection<Recipe> recipes;
        private ObservableCollection<Category> categories;
        private ObservableCollection<UnitOfMeasure> uoms;
        private static readonly Lazy<DataManager> sThis = new Lazy<DataManager>(() => new DataManager());

        public static DataManager Instance => sThis.Value;
        public ObservableCollection<Recipe> Recipes => recipes;
        public ObservableCollection<UnitOfMeasure> UnitOfMeasures => uoms;
        public ObservableCollection<Category> Categories => categories;

        private class DbManager : DbContext
        {
            public DbSet<Recipe> Recipes { get; set; }
            public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
            public DbSet<Category> Categories { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Filename=./cookr.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Recipe>().HasIndex(r => r.Name).IsUnique();
                modelBuilder.Entity<UnitOfMeasure>().HasIndex(u => u.Name).IsUnique();
                modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            }
        }

        public void SaveChangesToDB() { db.SaveChanges(); }

        private DataManager()
        {
            db = new DbManager();
            db.Database.EnsureCreated();
       
            var cat = db.Categories.ToList();
            var uom = db.UnitOfMeasures.ToList();
            var rec = db.Recipes.Include(r => r.Ingredients)
                     .Include(r => r.Steps)
                     .Include(r => r.Category);

            recipes = new ObservableCollection<Recipe>(rec);
            recipes.CollectionChanged += (sender, e) =>
            {
                if(e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach(var recipe in e.NewItems.Cast<Recipe>())
                        db.Add(recipe);
                    if (e.NewItems.Count > 0)
                        db.SaveChanges();
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var recipe in e.OldItems.Cast<Recipe>())
                        db.Remove(recipe);
                    if (e.OldItems.Count > 0)
                        db.SaveChanges();
                }
            };

            uoms = new ObservableCollection<UnitOfMeasure>(uom);
            uoms.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var um in e.NewItems.Cast<UnitOfMeasure>())
                        db.Add(um);
                    if (e.NewItems.Count > 0)
                        db.SaveChanges();
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var um in e.OldItems.Cast<UnitOfMeasure>())
                        db.Remove(um);
                    if (e.OldItems.Count > 0)
                        db.SaveChanges();
                }
            };

            categories = new ObservableCollection<Category>(cat);
            categories.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var category in e.NewItems.Cast<Category>())
                        db.Add(category);
                    if (e.NewItems.Count > 0)
                        db.SaveChanges();
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var category in e.OldItems.Cast<Category>())
                        db.Remove(category);
                    if (e.OldItems.Count > 0)
                        db.SaveChanges();
                }
            };
        }

        

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
