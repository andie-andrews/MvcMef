using System;
using System.Data.Entity;

namespace Netchex.Dependencies
{
    public interface IApplicationContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

     
    }
}
