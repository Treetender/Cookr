using Cookr.Lib.Commands;
using Cookr.wpf.AddIngredient;
using Cookr.wpf.AddRecipe;
using Cookr.wpf.AddStep;
using Core.data.DB;
using Core.data.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Cookr.wpf
{
    public class RecipeViewModel 
    {
        public ObservableCollection<Recipe> Recipes => SqliteDBManager.Instance.Recipes;
        public Recipe SelectedRecipe { get; set; }
        public Ingredient SelectedIngredient { get; set; }
        public Step SelectedStep { get; set; }

        public ICommand AddRecipeCommand => new RelayCommand(c => AddRecipe());
        public ICommand RemoveRecipeCommand => new RelayCommand(c => RemoveRecipe(), p => CanRemoveRecipe());
        public ICommand AddIngredientCommand => new RelayCommand(c => AddIngredient(), p => CanAddIngredient());
        public ICommand RemoveIngredientCommand => new RelayCommand(c => RemoveIngredient(), p => CanRemoveIngredient());
        public ICommand AddStepCommand => new RelayCommand(c => AddStep(), p => CanAddStep());
        public ICommand RemoveStepCommand => new RelayCommand(c => RemoveStep(), p => CanRemoveStep());

        private bool CanAddIngredient() => SelectedRecipe != null; 
        private void AddIngredient()
        {
            var vm = new AddIngredientViewModel(SelectedRecipe);
            var win = new AddIngredientWindow() { DataContext = vm };
            if(win.ShowDialog() ?? false)
            {
                SelectedRecipe.Ingredients.Add(vm.Ingredient);
                SqliteDBManager.Instance.SaveDBChanges();
            }
        }

        private bool CanAddStep() => SelectedRecipe != null;
        private void AddStep()
        {
            var vm = new AddStepViewModel(SelectedRecipe);
            var win = new AddStepWindow() { DataContext = vm };
            if(win.ShowDialog() ?? false)
            {
                SelectedRecipe.Steps.Add(vm.Step);
                SqliteDBManager.Instance.SaveDBChanges();
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

        private bool CanRemoveRecipe() => SelectedRecipe != null; 
        private void RemoveRecipe() { Recipes.Remove(SelectedRecipe); }

        private bool CanRemoveIngredient() => SelectedIngredient != null && SelectedRecipe != null;
        private void RemoveIngredient()
        {
            SelectedRecipe.Ingredients.Remove(SelectedIngredient);
            SqliteDBManager.Instance.SaveDBChanges();
        }

        private bool CanRemoveStep() => SelectedRecipe != null && SelectedStep != null;
        private void RemoveStep()
        {
            SelectedRecipe.Steps.Remove(SelectedStep);
            SqliteDBManager.Instance.SaveDBChanges();
        }
      
    }
}
