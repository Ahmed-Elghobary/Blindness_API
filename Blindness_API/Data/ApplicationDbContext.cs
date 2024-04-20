using Blindness_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Blindness_API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<LocalUser> localUsers { get; set; }
        public DbSet<MedicalTest> medicalTests { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
