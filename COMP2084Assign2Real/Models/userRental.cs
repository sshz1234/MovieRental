using System.ComponentModel.DataAnnotations;

namespace COMP2084Assign2Real.Models
{
    public class UserRental
    {
        public int UserRentalId { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string phone { get; set; }

        



    
    }
}
