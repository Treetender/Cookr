﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.data.Models
{
    public class Step
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Instructions { get; set; }
        public TimeSpan Time { get; set; } 

        public Ingredient Ingredient { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
    }
}
