namespace MvcMef.Dependencies
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    public interface IRepositoryWorker : IDisposable
    {
    
     void Dispose(bool disposing);
        IRepository<TEntity,TDto> Repository<TEntity,TDto>() where TEntity : class where TDto : class;
        dynamic ExecuteProcedrue(string sp, object[] paramaters);
        void Migrate<TContext, TConfiguration>() where TContext : DbContext where TConfiguration : DbMigrationsConfiguration<TContext>, new();

        dynamic ExecuteProcedrue(string sp);
    }


}
