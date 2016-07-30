using System;
using System.Data.Entity;

namespace MvcMef.Dependencies
{
    public interface IApplicationContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

     
    }
}
