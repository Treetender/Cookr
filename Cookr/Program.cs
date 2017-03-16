using Cookr.clr;
using Core.data.Models;
using System;
using System.Linq;

namespace Cookr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"We have currently {ClrDBManager.Instance.Recipes.Count()} recipes");

            var cookCategory = ClrDBManager.Instance.Categories.FirstOrDefault(c => c.Name.Equals("Dessert"));
            if (cookCategory == null)
                cookCategory = new Category() { Name = "Dessert" };

            var cookies = new Recipe()
            { 
                Category = cookCategory,
                Name = "Chocolate Chip Cookies #2"
            };

            if (ClrDBManager.Instance.Recipes.Any(r => r.Name.Equals(cookies.Name)))
                Console.WriteLine($"Recipe {cookies} is already in the DB.");
            else
            {
                ClrDBManager.Instance.Recipes.Add(cookies);
                ClrDBManager.Instance.SaveDBChanges();
            }

            Console.WriteLine($"We have currently {ClrDBManager.Instance.Recipes.Count()} recipes");
            Console.WriteLine($"Recipe ID for Cookies is {cookies.Id}");

            Console.WriteLine("Current Recipes:");
            foreach(var recipe in ClrDBManager.Instance.Recipes)
            {
                Console.WriteLine($"{recipe}");
            }
        
            Console.ReadLine();
        }
    }
}