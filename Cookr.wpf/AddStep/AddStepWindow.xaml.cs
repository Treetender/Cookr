using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cookr.wpf.AddStep
{
    /// <summary>
    /// Interaction logic for AddStepWindow.xaml
    /// </summary>
    public partial class AddStepWindow : Window
    {
        public AddStepWindow()
        {
            InitializeComponent();
            DataContextChanged += (sender, e) =>
            {
                var oldVM = e.OldValue as AddStepViewModel;
                var winVM = e.NewValue as AddStepViewModel;
                if (winVM != null)
                    winVM.WindowClosing += OnWindowClosing;
                if (oldVM != null)
                    oldVM.WindowClosing -= OnWindowClosing;
            };
        }

        private void OnWindowClosing(object sender, bool e)
        {
            DialogResult = e;
            Close();
        }
    }
}
