
using RetailSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class TransferListDto : EntityDto
    {
        public string TransferNumber { get; set; }
        
        public string Description { get; set; }
        
        public string Note { get; set; }

        public Status Status { get; set; }
        public string StatusName { get => Status.ToString(); }

        public int SourceLocationId { get; set; }
        
        public int DestinationLocationId { get; set; }

    }
}