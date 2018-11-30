using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class SubCategory : Entity
    {
        public SubCategory()
        {
            Items = new HashSet<Item>();
        }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}