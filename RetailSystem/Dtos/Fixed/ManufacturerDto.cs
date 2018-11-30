
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class ManufacturerDto
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int BusinessId { get; set; }
    }
}
