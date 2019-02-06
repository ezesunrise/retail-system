using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSystem.Dtos
{
    public class AppUserListDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public byte[] Photo { get; set; }

        public string Role { get; set; }

        public int? LocationId { get; set; }
        public string LocationName { get; set; }

        public int? BusinessId { get; set; }
        public string BusinessName { get; set; }

    }
}