using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace COMP2084Assign2Real.Models
{
    public class MovieRental
    {
        public int MovieRentalId { get; set; }

        [Required]
        public int owingAmount { get; set; }

        [Required]
        public string movieTitle { get; set; }

        [Required]
        public int dueDate { get; set; }

        // FK
        [Required]
        public int rentalDate { get; set; }

        // parent ref
        public UserRental UserRental { get; set; }
    }
}
