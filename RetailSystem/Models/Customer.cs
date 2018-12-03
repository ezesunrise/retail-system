using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Customer : Entity
    {
        //[Required]
        //[StringLength(7, MinimumLength = 7)]
        //public string CustomerNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [StringLength(128)]
        public string ContactPerson { get; set; } 

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string AlternatePhoneNumber { get; set; }

        public string Address { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

    }
}