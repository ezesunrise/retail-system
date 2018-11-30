using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class BusinessDto : EntityDto
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public byte[] Logo { get; set; }
    }
}