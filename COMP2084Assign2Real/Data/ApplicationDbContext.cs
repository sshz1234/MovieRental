using COMP2084Assign2Real.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace COMP2084Assign2Real.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserRental> userRental { get; set; }
        public DbSet<MovieRental> Rentals { get; set; }
        public DbSet<Movie> Movie { get; set; }
    }
}
