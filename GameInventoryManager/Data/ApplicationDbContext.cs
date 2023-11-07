

using GameInventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GameInventoryManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Games> games { get; set; }
    }
}
