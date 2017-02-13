using Cookr.lib.Models;
using System.Windows;
using System;

namespace Cookr.wpf.AddIngredient
{
    /// <summary>
    /// Interaction logic for AddIngredientWindow.xaml
    /// </summary>
    public partial class AddIngredientWindow : Window
    {
        public AddIngredientWindow()
        {
            InitializeComponent();
            DataContextChanged += (sender, e) =>
            {
                var oldVM = e.OldValue as AddIngredientViewModel;
                var newVM = e.NewValue as AddIngredientViewModel;

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
