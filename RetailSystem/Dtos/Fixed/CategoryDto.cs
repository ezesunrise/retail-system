﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class CategoryDto : EntityDto
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(AppConsts.CategoryCodeLength)]
        public string Code { get; set; }

        public int BusinessId { get; set; }
    }
}