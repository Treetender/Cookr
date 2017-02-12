using Cookr.lib.Models;
using System.Windows;

namespace Cookr.wpf.AddIngredient
{
    /// <summary>
    /// Interaction logic for AddIngredientWindow.xaml
    /// </summary>
    public partial class AddIngredientWindow : Window
    {
        private AddIngredientViewModel vm;

        public Ingredient Ingredient => vm.Ingredient;

        public AddIngredientWindow(Recipe ParentRecipe)
        {
            InitializeComponent();
            vm = new AddIngredientViewModel(ParentRecipe);
            vm.WindowClosing += (sender, e) =>
            {
                DialogResult = e;
                Close();
            };
        }
    }
}
