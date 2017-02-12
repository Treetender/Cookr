using Cookr.lib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Cookr.wpf.AddRecipe
{
    class AddRecipeViewModel
    {
        public ICommand AddRecipeCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand AddStepCommand { get; set; }
        public ICommand RemoveStepCommand { get; set; }
        public ICommand AddIngredientCommand { get; set; }
        public ICommand RemoveIngredientCommand { get; set; }

        public Recipe Recipe { get; set; }
        public IEnumerable<string> Categories => DataManager.Instance.Recipes.Select(r => r.Category).Distinct();


    }
}
