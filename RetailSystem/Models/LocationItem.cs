
using RetailSystem.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Models
{
    public class LocationItem : Entity
    {
        public LocationItem()
        {
            Condition = Condition.Good;
        }

        public int LocationId { get; set; }
        public int ItemId { get; set; }

        public decimal? UnitPrice { get; set; }

        public uint Quantity { get; set; }
        public uint LowQuantity { get; set; }
        public uint OptimumQuantity { get; set; }

        public Condition Condition { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual Item Item { get; set; }
    }
}