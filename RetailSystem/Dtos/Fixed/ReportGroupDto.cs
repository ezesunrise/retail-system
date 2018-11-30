using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class ReportGroupDto
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(64)]
        public string TotalCaption { get; set; }

    }
}