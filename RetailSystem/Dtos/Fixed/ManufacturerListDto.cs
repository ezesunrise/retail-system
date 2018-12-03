
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class ManufacturerListDto : EntityDto
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}
