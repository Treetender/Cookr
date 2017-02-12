using Cookr.lib.Models;
using Cookr.Lib.Commands;
using Cookr.wpf.AddIngredient;
using Cookr.wpf.AddRecipe;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Cookr.wpf
{
    public class RecipeViewModel 
    {
        public ObservableCollection<Recipe> Recipes => DataManager.Instance.Recipes;
        public Recipe SelectedRecipe { get; set; }
        public ICommand AddRecipeCommand => new RelayCommand(c => AddRecipe());
        public ICommand RemoveRecipeCommand => new RelayCommand(c => RemoveRecipe(), p => CanRemoveRecipe());
        public ICommand AddIngredientCommand => new RelayCommand(c => AddIngredient(), p => CanAddIngredient());


        private bool CanAddIngredient() { return SelectedRecipe != null; }
        private void AddIngredient()
        {
            var win = new AddIngredientWindow(SelectedRecipe);
            win.ShowDialog();
            if(win.DialogResult ?? false)
            {
                SelectedRecipe.Ingredients.Add(win.Ingredient);
            }
        }

        private void AddRecipe()
        {
            var vm = new AddRecipeViewModel();
            var win = new AddRecipeWindow() { DataContext = vm, Owner = Application.Current.MainWindow };
           
            if (win.ShowDialog() ?? false)
            {
                Recipes.Add(vm.Recipe);
                SelectedRecipe = vm.Recipe;
            }
        }

        private bool CanRemoveRecipe() { return SelectedRecipe != null; }
        private void RemoveRecipe() { Recipes.Remove(SelectedRecipe); }
    }
}
