using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class ReportItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int ReportGroupId { get; set; }
        public ReportGroup ReportGroup { get; set; }

    }
}