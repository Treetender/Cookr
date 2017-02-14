using System.ComponentModel.DataAnnotations.Schema;

namespace Core.data.Models
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

        public int UnitOfMeasureId { get; set; }
        public int RecipeId { get; set; }

        public override string ToString() => $"{Name} x{Quantity} {UoM}";
    }
}
