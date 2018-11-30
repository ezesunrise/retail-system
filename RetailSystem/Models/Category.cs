using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Category : Entity
    {
        public Category()
        {
            Items = new HashSet<Item>();
            SubCategories = new HashSet<SubCategory>();
        }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public int BusinessId { get; set; }

        public virtual Business Business { get; set; }
        
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}