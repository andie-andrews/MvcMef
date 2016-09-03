using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace MvcMef.Dependencies
{
    public interface IApplicationContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
        Database Database { get; }

        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }

        DbSet Set(Type entityType);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbEntityEntry Entry(object entity);

        string ToString();
        bool Equals(object obj);
        int GetHashCode();
        Type GetType();


    }
}
