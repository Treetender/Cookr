using System;
using Microsoft.EntityFrameworkCore;
using Core.data.DB;
using Core.data.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cookr.clr
{
    class ClrDBManager : DBManager
    {
        private ObservableCollection<Recipe> recipes;
        private ObservableCollection<Category> categories;
        private ObservableCollection<UnitOfMeasure> uoms;

        protected override Db DataContext { get; set; }

        private class SqliteDB : Db
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Filename=./cookr_clr.db");
            }
           
        }

        private static Lazy<ClrDBManager> sThis = new Lazy<ClrDBManager>(() => new ClrDBManager());
        private ClrDBManager()
        {
            DataContext = new SqliteDB();
            DataContext.Database.EnsureCreated();

            recipes = new ObservableCollection<Recipe>(DataContext.Recipes.ToList());
            recipes.CollectionChanged += OnRecipesChanged;

            categories = new ObservableCollection<Category>(DataContext.Categories.ToList());
            categories.CollectionChanged += OnCategoriesChanged;

            uoms = new ObservableCollection<UnitOfMeasure>(DataContext.UnitOfMeasures.ToList());
            uoms.CollectionChanged += OnUnitOfMeasuresChanged;
        }

        public static ClrDBManager Instance => sThis.Value;
        public override ObservableCollection<Recipe> Recipes => recipes;
        public override ObservableCollection<Category> Categories => categories;
        public override ObservableCollection<UnitOfMeasure> UoMs => uoms;


    }
}
