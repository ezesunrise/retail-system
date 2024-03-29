﻿using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class BusinessListDto : EntityDto
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public Status Status { get; set; }
        public string StatusName { get => Status.ToString(); }
    }
}