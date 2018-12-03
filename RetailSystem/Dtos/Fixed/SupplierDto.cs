using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SupplierDto : EntityDto
    {
        [Required]
        public string SupplierCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public string EmailAddress { get; set; }

        [StringLength(128)]
        public string ContactPerson { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string AlternatePhoneNumber { get; set; }

        public string Address { get; set; }

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

        public int BusinessId { get; set; }
        
    }
}