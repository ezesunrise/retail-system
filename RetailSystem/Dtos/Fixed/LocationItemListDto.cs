
using RetailSystem.Models.Enums;

namespace RetailSystem.Dtos
{
    public class LocationItemListDto
    {
        public int LocationId { get; set; }
        public int ItemId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public int AvailableQuantity { get => Quantity - FaultQuantity; }
        public int FaultQuantity { get; set; }
        public int LowQuantity { get; set; }
        public int OptimumQuantity { get; set; }

        public Status Status { get; set; }
        public string StatusName { get => Status.ToString(); }

    }
}