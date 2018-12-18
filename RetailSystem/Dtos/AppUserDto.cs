using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class AppUserDto
    {
        public string Id { get; set; }

        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

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

        public byte[] Photo { get; set; }
    }
}