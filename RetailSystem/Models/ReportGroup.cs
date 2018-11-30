using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class ReportGroup : Entity
    {
        public ReportGroup()
        {
            ReportItems = new HashSet<ReportItem>();
        }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(64)]
        public string TotalCaption { get; set; }

        public virtual ICollection<ReportItem> ReportItems { get; set; }
    }
}