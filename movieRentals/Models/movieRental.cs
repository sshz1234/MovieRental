using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace COMP2084Assign2Real.Models
{
    public class MovieRental
    {
        public int MovieRentalId { get; set; }

        [Required]
        [Display(Name="owing amount: ")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal owingAmount { get; set; }


        

        [Required]
        [Display(Name = "Return date: ")]
        public DateTime dueDate { get; set; }


        // FK
        [Required]
        [Display(Name = "Your email: ")]
        public int UserRentalId { get; set; }
        [Display(Name = "Name of Movie: ")]
        public int MovieId { get; set; }
        [Required]
        [Display(Name = "Date rented: ")]
        public DateTime rentalDate { get; set; }


        // parent ref
        [Display(Name = "email of renter: ")]
        public UserRental? UserRental { get; set; }
        public Movie? Movie { get; set; }
    }
}
