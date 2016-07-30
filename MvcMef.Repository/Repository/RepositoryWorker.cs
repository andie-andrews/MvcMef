namespace MvcMef.Repository.Repository
{
    using System;
    using System.Collections;
    using System.ComponentModel.Composition;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using MvcMef.Dependencies;

    [Export(typeof(IRepositoryWorker))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class RepositoryWorker : IRepositoryWorker
    {
        private readonly IApplicationContext _context;
 
        private bool _disposed;
        private Hashtable _repositories;

        private MappingProvider _provider;
 
        [ImportingConstructor]
        public RepositoryWorker(IApplicationContext context,  MappingProvider provider)
        {
            this._context = context;
            this._provider = provider;
        }
   
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
 
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
                if (disposing)
                    this._context.Dispose();
 
            this._disposed = true;
        }

        public IRepository<TEntity, TDto> Repository<TEntity, TDto>() where TEntity : class where TDto : class
        {
            if (this._repositories == null)
                this._repositories = new Hashtable();
 
            var type = typeof (TEntity).Name;
 
            if (!this._repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<,>);
 
                var repositoryInstance = 
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(TEntity), typeof(TDto)), this._context, this._provider);
                 
                this._repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity, TDto>)this._repositories[type];
        }

        public void Migrate<TContext, TConfiguration>() where TContext : DbContext where TConfiguration : DbMigrationsConfiguration<TContext>, new()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TConfiguration>()); 
        }
    }
}

