using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SubCategoryListDto : EntityDto
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}