using Cookr.clr;
using Cookr.data.Models;
using System;
using System.Linq;

namespace Cookr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"We have currently {DataManager.Instance.Recipes.Count()} recipes");

            var cookies = new Recipe()
            {
                Category = "Dessert",
                Name = "Chocolate Chip Cookies"
            };

            if (DataManager.Instance.Recipes.Contains(cookies))
                Console.WriteLine($"Recipe {cookies} is already in the DB.");
            else
            {
                DataManager.Instance.Database.Add(cookies);
                DataManager.Instance.Database.SaveChanges();
            }

            Console.WriteLine($"We have currently {DataManager.Instance.Recipes.Count()} recipes");
            Console.WriteLine($"Recipe ID for Cookies is {cookies.Id}");
        
            Console.ReadLine();
        }
    }
}