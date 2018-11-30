using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Unit : Entity
    {
        public Unit()
        {
            Items = new HashSet<Item>();
        }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public int? BusinessId { get; set; }
        public Business Business { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}