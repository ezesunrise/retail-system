using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System.Collections.Generic;

namespace RetailSystem.Dtos
{
    public class LocationListDto : EntityDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public LocationType Type { get; set; }
        public string TypeName { get => Type.ToString(); }

        public Status Status { get; set; }
        public string StatusName { get => Status.ToString(); }

        public decimal? Target { get; set; }
        public TargetType? TargetType { get; set; }
        public string TargetTypeName { get => TargetType.ToString(); }

        public string Address { get; set; }
        
        public string PhoneNumber1 { get; set; }
        
        public string PhoneNumber2 { get; set; }

        public string ContactPerson { get; set; }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}