using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class RegisterDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public Status Status { get; set; }
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

        public string Role { get; set; }

        public int? LocationId { get; set; }

        public int? BusinessId { get; set; }

    }
}