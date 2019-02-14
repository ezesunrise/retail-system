using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class CategoryListDto : EntityDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Code { get; set; }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}