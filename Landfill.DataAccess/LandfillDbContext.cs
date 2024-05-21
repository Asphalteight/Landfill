using Landfill.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public override int SaveChanges()
        {
            foreach (var entityEntry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is BaseEntityWithTime))
            {
                ((BaseEntityWithTime)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
            }
            foreach (var entityEntry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is BaseEntityWithTime))
            {
                ((BaseEntityWithTime)entityEntry.Entity).UpdatedOn = DateTime.UtcNow;
            }

            return SaveChanges(true);
        }
    }
}
