using Cookr.lib.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Cookr.wpf
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        public IEnumerable<Recipe> Recipes => DataManager.Instance.Recipes;
        public ICommand AddRecipeCommand { get; set; }
        public ICommand RemoveRecipeCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
