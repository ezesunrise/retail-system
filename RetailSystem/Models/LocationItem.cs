
using RetailSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Models
{
    public class LocationItem
    {
        public LocationItem()
        {
            Status = Status.Active;
        }

        public int LocationId { get; set; }
        public int ItemId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, int.MaxValue)]
        public int FaultQuantity { get; set; }
        [Range(0, int.MaxValue)]
        public int OptimumQuantity { get; set; }
        [Range(0, int.MaxValue)]
        public int LowQuantity { get; set; }

        public Status Status { get; set; }

        [Range(0, int.MaxValue)]
        public byte? DiscountQuantity { get; set; }
        public byte PercentDiscount { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual Item Item { get; set; }
    }
}