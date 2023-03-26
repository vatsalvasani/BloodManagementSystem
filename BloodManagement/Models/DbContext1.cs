using Microsoft.EntityFrameworkCore;
namespace BloodManagement.Models
{
    public class DbContext1 : DbContext
    {
        public DbContext1(DbContextOptions<DbContext1> options) : base(options)
        {
        }

        

        public DbSet<User1> User1 { get; set; } = null!;
        public DbSet<Hospital> Hospital { get; set; } = null!;

        public DbSet<Donor> Donor { get; set; } = null!;
        public DbSet<Beneficiary> Beneficiary { get; set; } = null!;
        public DbSet<Review1> Review1 { get; set; } = null!;
    }
}
