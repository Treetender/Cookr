using Cookr.Lib.Commands;
using Core.data.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Cookr.wpf.AddIngredient
{
    class AddIngredientViewModel
    {
        public event EventHandler<bool> WindowClosing;
        public IEnumerable<UnitOfMeasure> UnitOfMeasures => SqliteDBManager.Instance.UoMs;

        public Ingredient Ingredient { get; private set; }
        public ICommand AddCommand => new RelayCommand(c => AddIngredient(), p => CanAddIngredient());
        public ICommand CancelCommand => new RelayCommand(c => CloseWindow());

        [Obsolete("Use for designer only", true)]
        public AddIngredientViewModel() : this(new Recipe() { Name = "Test Recipe"}) { }
        public AddIngredientViewModel(Recipe Recipe)
        {
            Ingredient = new Ingredient() { Recipe = Recipe, Quantity = 0 };
        }

        private bool CanAddIngredient()
        {
            return (Ingredient.Name?.Length ?? 0) > 0
                && Ingredient.Quantity > 0
                && Ingredient.UoM != null;
        }

        private void AddIngredient() { WindowClosing?.Invoke(this, true); }
        private void CloseWindow() { WindowClosing?.Invoke(this, false); }
    }
}
