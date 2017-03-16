using Core.data.DB;
using System;
using Core.data.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cookr.wpf
{
    public class SqliteDBManager : DBManager
    {
        private ObservableCollection<Recipe> recipes;
        private ObservableCollection<Category> categories;
        private ObservableCollection<UnitOfMeasure> uoms;

        protected override Db DataContext { get; set; }

        private SqliteDBManager() : base()
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
        private static Lazy<SqliteDBManager> sThis = new Lazy<SqliteDBManager>(() => new SqliteDBManager());

        private class SqliteDB : Db
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Filename=./cookr.db");
            }

        }

        public static SqliteDBManager Instance => sThis.Value;

        public override ObservableCollection<Recipe> Recipes => recipes;
        public override ObservableCollection<Category> Categories => categories;
        public override ObservableCollection<UnitOfMeasure> UoMs => uoms;
    }
}

