using System.ComponentModel.DataAnnotations;

namespace COMP2084Assign2Real.Models
{
    public class UserRental
    {
        public int UserRentalId { get; set; }

        [Required]
        public int owingAmount { get; set; }

        [Required]
        public decimal movieTitle { get; set; }

        [Required]
        public string dueDate { get; set; }

        // FK
        [Required]
        public int rentalDate { get; set; }

        public List<MovieRental>? movieRentals { get; set; }

    }
}
