namespace MvcMef.Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Dependencies;
    using Data;
    using Dependencies.Models;
    public class Repository<TEntity, TDto> : IRepository<TEntity, TDto>
        where TEntity : class
        where TDto : class, IIdentity
    {
        internal IApplicationContext Context;
        internal IDbSet<TEntity> DbSet;
        internal IMapping<TEntity, TDto> EntityToDtoMapping;

        public Repository(IApplicationContext context, MappingProvider mappingProvider)
        {

            Context = context as IApplicationContext;
            DbSet = context.Set<TEntity>();
            EntityToDtoMapping = mappingProvider.ProvideProcessor(typeof(TEntity), typeof(TDto)) as IMapping<TEntity, TDto>;
        }

        public virtual TDto FindById(object id)
        {
            return EntityToDtoMapping.Map(DbSet.Find(id));
        }

        public virtual TEntity InsertGraph(TDto source)
        {
            TEntity entity = EntityToDtoMapping.Map(source);
            DbSet.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual TEntity Update(TDto source)
        {
            TEntity entity = EntityToDtoMapping.Map(source);
            var attachment = DbSet.Find(source.Id);

            // Activity does not exist in database and it's new one
            if (attachment == null)
            {
                DbSet.Add(entity);
                return entity;
            }

            // Activity already exist in database and modify it
            Context.Entry(attachment).CurrentValues.SetValues(entity);
            Context.Entry(attachment).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public virtual void Delete(object id)
        {

            var entity = DbSet.Find(id);
            var objectState = entity as IObjectState;
            if (objectState != null)
                objectState.State = ObjectState.Deleted;
            Delete((TEntity)entity);
            Context.SaveChanges();

        }

        public virtual void Delete(TDto source)
        {
            TEntity entity = EntityToDtoMapping.Map(source);
            DbSet.Attach(entity);
            DbSet.Remove(entity);
            Context.SaveChanges();

        }

        public virtual TEntity Insert(TDto source)
        {
            TEntity entity = EntityToDtoMapping.Map(source);
            DbSet.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual IRepositoryQuery<TEntity, TDto> Query()
        {
            RepositoryQuery<TEntity, TDto> repositoryGetFluentHelper =
                new RepositoryQuery<TEntity, TDto>(this, EntityToDtoMapping);

            return repositoryGetFluentHelper;
        }

        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>>
                includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }
    }

}
