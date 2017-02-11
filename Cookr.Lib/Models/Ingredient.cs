using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cookr.lib.Models
{
    /// <summary>
    /// An ingredient for a Recipe with a given quantity and UoM
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Unique Identity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Name of Inredient
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Amount of Ingredient
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// UoM for the ingredient
        /// </summary>
        public UnitOfMeasure UoM { get; set; }
        /// <summary>
        /// Parent Recipe
        /// </summary>
        public Recipe Recipe { get; set; }
    }
}
