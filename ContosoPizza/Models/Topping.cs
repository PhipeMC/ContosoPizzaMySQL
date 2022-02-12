using System;
using System.Collections.Generic;

namespace ContosoPizza.Models
{
    public partial class Topping
    {
        public Topping()
        {
            Pizzas = new HashSet<Pizza>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Calories { get; set; } = null!;

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
