using Cookr.lib.Models;
using Cookr.Lib.Commands;
using Cookr.wpf.AddIngredient;
using System.Collections.Generic;
using System.Windows.Input;

namespace Cookr.wpf
{
    public class RecipeViewModel 
    {
        public IEnumerable<Recipe> Recipes => DataManager.Instance.Recipes;
        public Recipe SelectedRecipe { get; set; }
        public ICommand AddRecipeCommand { get; set; }
        public ICommand RemoveRecipeCommand { get; set; }
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

        }
    }
}
