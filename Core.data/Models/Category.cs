using System;
using System.Collections.Generic;
using System.Text;

namespace Core.data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
