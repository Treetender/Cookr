using Core.data.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Core.data.DB
{
    public abstract class DBManager : IDisposable
    {
        protected abstract Db DataContext { get; set; }

        protected DBManager() { }

        protected virtual void OnRecipesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add && e.NewItems?.Count > 0)
                foreach (var recipe in e.NewItems.Cast<Recipe>()) { DataContext.Recipes.Add(recipe); }
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems?.Count > 0)
                foreach (var recipe in e.OldItems.Cast<Recipe>()) { DataContext.Recipes.Remove(recipe); }
        }

        protected virtual void OnUnitOfMeasuresChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems?.Count > 0)
                foreach (var uom in e.NewItems.Cast<UnitOfMeasure>()) { DataContext.UnitOfMeasures.Add(uom); }
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems?.Count > 0)
                foreach (var uom in e.OldItems.Cast<UnitOfMeasure>()) { DataContext.UnitOfMeasures.Remove(uom); }
        }

        protected virtual void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems?.Count > 0)
                foreach (var category in e.NewItems.Cast<Category>()) { DataContext.Categories.Add(category); }
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems?.Count > 0)
                foreach (var category in e.OldItems.Cast<Category>()) { DataContext.Categories.Remove(category); }
        }

        public abstract ObservableCollection<Recipe> Recipes { get; }
        public abstract ObservableCollection<Category> Categories { get; }
        public abstract ObservableCollection<UnitOfMeasure> UoMs { get; }

        public virtual async void SaveDBChangedAsync()
        {
            try { await DataContext.SaveChangesAsync(); }
            catch { throw; }
        }

        public virtual void SaveDBChanges()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch { throw; }
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && DataContext != null)
                    DataContext.Dispose();

                Recipes?.Clear();
                Categories?.Clear();
                UoMs?.Clear();

                Recipes.CollectionChanged -= OnRecipesChanged;
                Categories.CollectionChanged -= OnCategoriesChanged;
                UoMs.CollectionChanged -= OnUnitOfMeasuresChanged;
                
                disposedValue = true;
            }
        }

        /// <summary>
        /// Releases all memory managed objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
