using System.ComponentModel.DataAnnotations;

namespace COMP2084Assign2Real.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required]
        [Display(Name = "duration time(in mins): ")]
        public int duration { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        
       
        public string rating { get; set; }
       
    
    }
}
