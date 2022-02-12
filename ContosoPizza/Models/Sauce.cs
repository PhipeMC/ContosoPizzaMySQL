using System;
using System.Collections.Generic;

namespace ContosoPizza.Models
{
    public partial class Sauce
    {
        public Sauce()
        {
            Pizzas = new HashSet<Pizza>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ulong IsVegan { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
