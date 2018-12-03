
using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;

namespace RetailSystem.Dtos
{
    public class LocationItemDto
    {
        public int LocationId { get; set; }
        public int ItemId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public int FaultQuantity { get; set; }
        public int AvailableQuantity { get => Quantity - FaultQuantity; }
        public int LowQuantity { get; set; }
        public int OptimumQuantity { get; set; }

        public int Status { get; set; }
        public IList<KeyValuePairDto> StatusList
        {
            get
            {
                IList<KeyValuePairDto> list = new List<KeyValuePairDto>();
                foreach (int value in Enum.GetValues(typeof(Status)))
                {
                    list.Add(new KeyValuePairDto { Value = value, DisplayName = Enum.GetName(typeof(Status), value) });
                }
                return list;
            }
        }

    }
}