using System.ComponentModel.DataAnnotations;

namespace COMP2084Assign2Real.Models
{
    public class userRental
    {
        public int movieRentalId { get; set; }

        [Required]
        public int owingAmount { get; set; }

        [Required]
        public decimal movieTitle { get; set; }

        [Required]
        public string dueDate { get; set; }

        // FK
        [Required]
        public int rentalDate { get; set; }

        public ICollection<movieRental> Rentals { get; set; }

    }
}
