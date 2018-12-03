using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem.Services.Computation
{
    public static class Computer
    {
        public static void ComputeSale(Sale sale, IEnumerable<LocationItem> locationItems)
        {
            //var itemIds = sale.SaleItems.Select(s => s.ItemId);
            //if (locationItems.Count() != itemIds.Count())
            //{
            //    return BadRequest("Duplicate item in sale or an item does not exist");
            //}

            //foreach (var item in locationItems)
            //{
            //    item.Quantity -= entityDto.SaleItems.Single(s => s.ItemId == item.ItemId).Quantity;
            //    if (item.Quantity < 0)
            //    {
            //        return BadRequest(new { message = "Insufficient quantity", item });
            //    }
            //    _locationItemRepository.Update(item);
            //}

        }
    }
}
