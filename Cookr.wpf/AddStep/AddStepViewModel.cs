using Cookr.lib.Models;
using Cookr.Lib.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cookr.wpf.AddStep
{
    class AddStepViewModel
    {
        private int minutes = 0, hours = 0;
        public event EventHandler<bool> WindowClosing;

        public ICommand CloseWindowCommand => new RelayCommand(c => CloseWindow());
        public ICommand AddStepCommand => new RelayCommand(c => AddStep(), p => CanAddStep());
        public Step Step { get; private set; }
        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                Step.Time = new TimeSpan(0, hours, minutes, 0, 0);
            }
        }
        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                Step.Time = new TimeSpan(0, hours, minutes, 0, 0);
            }
        }
        public IEnumerable<Ingredient> Ingredients => Step.Recipe?.Ingredients ?? new ObservableCollection<Ingredient>();

        [Obsolete("Use for the designer only", true)]
        public AddStepViewModel() 
            : this(new Recipe()
            {
                Name = "Test Recipe for Designer",
            })
        {
            var ea = new UnitOfMeasure() { Name = "Each" };
            var tsp = new UnitOfMeasure() { Name = "Teaspoon" };
            var cup = new UnitOfMeasure() { Name = "Cup" };
            Step.Recipe.Ingredients.Add(new Ingredient() { Name = "Eggs", Quantity = 2, UoM = ea, Recipe = Step.Recipe });
            Step.Recipe.Ingredients.Add(new Ingredient() { Name = "Salt", Quantity = 2.25, UoM = tsp, Recipe = Step.Recipe });
            Step.Recipe.Ingredients.Add(new Ingredient() { Name = "Chocolate Chips", Quantity = 1.5, UoM = cup, Recipe = Step.Recipe });
        }
        public AddStepViewModel(Recipe recipe)
        {
            Step = new Step()
            {
                Recipe = recipe,
                RecipeId = recipe.Id,
                Time = new TimeSpan(0)
            };
            Hours = 0;
            Minutes = 0;
        }

        private bool CanAddStep()
        {
            return (Step.Instructions?.Length ?? 0) > 0 
                && Step.Time.Ticks > 0;
        }

        private void AddStep() { WindowClosing?.Invoke(this, true); }
        private void CloseWindow() { WindowClosing?.Invoke(this, false); }
    }
}
