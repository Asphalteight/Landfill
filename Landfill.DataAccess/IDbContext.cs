using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace Landfill.DataAccess
{
    public interface IDbContext
    {
        IQueryable<T> QuerySet<T>() where T : class;

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
