using Microsoft.EntityFrameworkCore;
using VacationSystem.Models;

namespace VacationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<PlanningSuggestion> PlanningSuggestions { get; set; }

    }
}
