
using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class TransferDto : EntityDto
    {
        [Required]
        public string TransferNumber { get; set; }
        
        [StringLength(512)]
        public string Note { get; set; }

        public Status Status { get; set; }
        
        public int SourceLocationId { get; set; }
        
        public int DestinationLocationId { get; set; }

        public ICollection<TransferItemDto> TransferItems { get; set; }
    }
}