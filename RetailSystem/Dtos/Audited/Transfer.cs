
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class TransferDto
    {
        [Required]
        public string ReferenceNumber { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int Status { get; set; }
        
        public int SourceLocationId { get; set; }
        
        public int DestinationLocationId { get; set; }

    }
}