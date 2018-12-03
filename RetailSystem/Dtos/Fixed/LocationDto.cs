using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;

namespace RetailSystem.Dtos
{
    public class LocationDto : EntityDto
    {
        public string Name { get; set; }

        public int Type { get; set; }
        public IList<KeyValuePairDto> TypeList
        {
            get
            {
                IList<KeyValuePairDto> list = new List<KeyValuePairDto>();
                foreach (int value in Enum.GetValues(typeof(LocationType)))
                {
                    list.Add(new KeyValuePairDto { Value = value, DisplayName = Enum.GetName(typeof(LocationType), value) });
                }
                return list;
            }
        }

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

        public decimal? Target { get; set; }
        public int? TargetType { get; set; }

        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string AlternatePhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public int BusinessId { get; set; }
    }
}