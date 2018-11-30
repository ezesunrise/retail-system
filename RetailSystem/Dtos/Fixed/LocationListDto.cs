using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RetailSystem.Dtos
{
    public class LocationListDto
    {
        public string Name { get; set; }

        public int Type { get; set; }
        public int Status { get; set; }

        public decimal? Target { get; set; }
        public int? TargetType { get; set; }

        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string AlternatePhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public int BusinessId { get; set; }
    }
}