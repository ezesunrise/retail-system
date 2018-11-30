
namespace RetailSystem.Dtos
{
    public class LocationItemDto
    {
        public int LocationId { get; set; }
        public int ItemId { get; set; }

        public decimal? UnitPrice { get; set; }

        public uint Quantity { get; set; }
        public uint LowQuantity { get; set; }
        public uint OptimumQuantity { get; set; }

        public int Condition { get; set; }

    }
}