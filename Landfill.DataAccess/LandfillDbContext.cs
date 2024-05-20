using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Landfill.DataAccess
{
    public class LandfillDbContext : DbContext, IDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=localhost; Database=landfill; Integrated Security=True; Encrypt=false");
            builder.LogTo(m => Debug.WriteLine(m));
            base.OnConfiguring(builder);
        }

        public IQueryable<TEntity> QuerySet<TEntity>() where TEntity : class => Set<TEntity>();

        public override int SaveChanges() => SaveChanges(true);
    }
}
