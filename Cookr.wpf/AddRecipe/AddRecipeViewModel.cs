using Cookr.Lib.Commands;
using Cookr.wpf.AddIngredient;
using Cookr.wpf.AddStep;
using Core.data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cookr.wpf.AddRecipe
{
    class AddRecipeViewModel
    {
        public event EventHandler<bool> WindowClosing;

        public ICommand AddRecipeCommand => new RelayCommand(c => AddRecipe(), p => CanAddRecipe());
        public ICommand CloseWindowCommand => new RelayCommand(c => CloseWindow());
        public ICommand AddStepCommand => new RelayCommand(c => AddStep());
        public ICommand RemoveStepCommand => new RelayCommand(c => RemoveStep(), p => CanRemoveStep());
        public ICommand AddIngredientCommand => new RelayCommand(c => AddIngredient());
        public ICommand RemoveIngredientCommand => new RelayCommand(c => RemoveIngredient(), p => CanRemoveIngredient());

        public Recipe Recipe { get; private set; }
        public Step SelectedStep { get; set; }
        public Ingredient SelectedIngredient { get; set; }
        public IEnumerable<Category> Categories => DataManager.Instance.Categories;

        public AddRecipeViewModel()
        {
            Recipe = new Recipe();
            Recipe.Ingredients = new ObservableCollection<Ingredient>();
            Recipe.Steps = new ObservableCollection<Step>();
        }

        private void CloseWindow() { WindowClosing?.Invoke(this, false); }
        private bool CanAddRecipe()
        {
            return Recipe.Ingredients.Count > 0
                && Recipe.Steps.Count > 0
                && (Recipe.Name?.Length ?? 0) > 0
                && Recipe.Category != null;
        }

        private void AddRecipe() { WindowClosing?.Invoke(this, true); }

        private void AddStep()
        {
            var vm = new AddStepViewModel(Recipe);
            var win = new AddStepWindow() { DataContext = vm };
            if(win.ShowDialog() ?? false)
            {
                Recipe.Steps.Add(vm.Step);
            }
        }

        private void AddIngredient()
        {
            var vm = new AddIngredientViewModel(Recipe);
            var win = new AddIngredientWindow() { DataContext = vm };
            if (win.ShowDialog() ?? false)
            {
                Recipe.Ingredients.Add(vm.Ingredient);
            }
        }

        private bool CanRemoveStep() { return SelectedStep != null; }
        private void RemoveStep() { Recipe.Steps.Remove(SelectedStep); }

        private bool CanRemoveIngredient() { return SelectedIngredient != null; }
        private void RemoveIngredient() { Recipe.Ingredients.Remove(SelectedIngredient); }
    }
}
