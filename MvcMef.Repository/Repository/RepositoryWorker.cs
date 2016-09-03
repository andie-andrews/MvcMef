using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MvcMef.Repository.Repository
{
    using System;
    using System.Collections;
    using System.ComponentModel.Composition;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using Dependencies;

    [Export(typeof(IRepositoryWorker))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RepositoryWorker : IRepositoryWorker
    {
        private readonly IApplicationContext _context;

        private bool _disposed;
        private Hashtable _repositories;

        private MappingProvider _provider;

        [ImportingConstructor]
        public RepositoryWorker(IApplicationContext context, MappingProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public dynamic ExecuteProcedrue(string sp, object[] paramaters)
        {
            StringBuilder sb = new StringBuilder(sp);
            paramaters.Where(p => p is SqlParameter).ToList().ForEach(p =>
            {
                sb.Append(" ").Append(((SqlParameter)p).ParameterName).Append(",");

            });
            sb.Length--;
            return _context.Database.ExecuteSqlCommand(sb.ToString(), paramaters);
        }

        public dynamic ExecuteProcedrue(string sp)
        {
            return _context.Database.ExecuteSqlCommand(sp);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public IRepository<TEntity, TDto> Repository<TEntity, TDto>() where TEntity : class where TDto : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<,>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(TEntity), typeof(TDto)), _context, _provider);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity, TDto>)_repositories[type];
        }

        public void Migrate<TContext, TConfiguration>() where TContext : DbContext where TConfiguration : DbMigrationsConfiguration<TContext>, new()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TConfiguration>());
        }
    }
}

