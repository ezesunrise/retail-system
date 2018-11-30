using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem.Models
{
    public class Manufacturer : Entity
    {
        public Manufacturer()
        {
            Items = new HashSet<Item>();
        }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
