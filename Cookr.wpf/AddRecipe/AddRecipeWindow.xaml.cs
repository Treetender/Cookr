using System;
using System.Windows;

namespace Cookr.wpf.AddRecipe
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
            DataContextChanged += (sender, e) =>
            {
                var oldVM = e.OldValue as AddRecipeViewModel;
                var newVM = e.NewValue as AddRecipeViewModel;

                if (oldVM != null)
                    oldVM.WindowClosing -= OnWindowClosing;
                if (newVM != null)
                    newVM.WindowClosing += OnWindowClosing;
            };
        }

        private void OnWindowClosing(object sender, bool e)
        {
            DialogResult = e;
            Close();
        }
    }
}
