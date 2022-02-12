using System;
using System.Collections.Generic;

namespace ContosoPizza.Models
{
    public partial class Pizza
    {
        public Pizza()
        {
            Toppings = new HashSet<Topping>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? SauceId { get; set; }

        public virtual Sauce? Sauce { get; set; }

        public virtual ICollection<Topping> Toppings { get; set; }
    }
}
