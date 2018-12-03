using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System.Collections.Generic;

namespace RetailSystem.Dtos
{
    public class LocationListDto : EntityDto
    {
        public string Name { get; set; }

        public int Type { get; set; }
        public string TypeName { get => Type.ToString(); }

        public Status Status { get; set; }
        public string StatusName { get => Status.ToString(); }

        public decimal? Target { get; set; }
        public int? TargetType { get; set; }

        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string AlternatePhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}