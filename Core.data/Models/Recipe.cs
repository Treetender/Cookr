﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.data.Models
{
    public class Recipe : IEquatable<Recipe>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ObservableCollection<Step> Steps { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public bool Equals(Recipe other)
        {
            if (other == null)
                return false;
            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return Equals(obj as Recipe);
        }

        public override int GetHashCode()
        {
            return 13 * Id.GetHashCode()
                 ^ 29 * (Name?.GetHashCode() ?? 0)
                 ^ 17 * (Category?.GetHashCode() ?? 0);
        }

        public static bool operator ==(Recipe r1, Recipe r2)
        {
            if (ReferenceEquals(r1, r2))
                return true;
            if (((object)r1 == null || (object)r2 == null))
                return false;
            return r1.Equals(r2);
        }

        public static bool operator !=(Recipe r1, Recipe r2)
        {
            return !(r1 == r2);
        }

        public override string ToString() => Name;
    }
}
