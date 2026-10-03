using Microsoft.EntityFrameworkCore;
using AdvancedSharpLab.Models;

namespace AdvancedSharpLab.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
