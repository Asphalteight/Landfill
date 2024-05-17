using Microsoft.EntityFrameworkCore;

namespace Landfill.DataAccess
{
    public class LandfillDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=localhost; Database=landfill; Integrated Security=True; Encrypt=false");
            base.OnConfiguring(builder);
        }
    }
}
