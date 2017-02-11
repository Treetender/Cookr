using System.ComponentModel.DataAnnotations.Schema;

namespace Cookr.lib.Models
{
    /// <summary>
    /// Represents a single unit of measure (e.g. cup, tsp, tbsp, ea)
    /// </summary>
    public class UnitOfMeasure
    {
        /// <summary>
        /// Unique Identity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Unit of Measure description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Converts Unit of Measure into a string
        /// </summary>
        /// <returns>The UoM Name</returns>
        public override string ToString() { return Name; }
    }
}
