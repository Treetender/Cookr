using Cookr.lib.Models;
using Cookr.Lib.Commands;
using Cookr.wpf.AddStep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Cookr.wpf.AddRecipe
{
    class AddRecipeViewModel
    {
        public event EventHandler<bool> WindowClosing;

        public ICommand AddRecipeCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand AddStepCommand => new RelayCommand(c => AddStep());
        public ICommand RemoveStepCommand { get; set; }
        public ICommand AddIngredientCommand { get; set; }
        public ICommand RemoveIngredientCommand { get; set; }

        public Recipe Recipe { get; private set; }
        public IEnumerable<Category> Categories => DataManager.Instance.Categories;

        public AddRecipeViewModel()
        {
            Recipe = new Recipe();
            Recipe.Ingredients = new List<Ingredient>();
            Recipe.Steps = new List<Step>();
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
    }
}
