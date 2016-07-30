namespace MvcMef.Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using MvcMef.Dependencies;
    using MvcMef.Repository.Repository.Data;
    using Dependencies.Models;
    public class Repository<TEntity, TDto> : IRepository<TEntity, TDto>
        where TEntity : class
        where TDto : class, IIdentity
    {
        internal DbContext Context;
        internal IDbSet<TEntity> DbSet;
        internal IMapping<TEntity,TDto> EntityToDtoMapping;

        public Repository(IApplicationContext context, MappingProvider mappingProvider)
        {
           
            this.Context = context as DbContext;
            this.DbSet = context.Set<TEntity>();
            this.EntityToDtoMapping = mappingProvider.ProvideProcessor(typeof(TEntity), typeof(TDto)) as IMapping<TEntity, TDto>;
        }

        public virtual TDto FindById(object id)
        {
            return this.EntityToDtoMapping.Map(this.DbSet.Find(id));
        }

        public virtual TEntity InsertGraph(TDto source)
        {
            TEntity entity = this.EntityToDtoMapping.Map(source);
            this.DbSet.Add(entity);
            this.Context.SaveChanges();

            return entity;
        }

        public virtual TEntity Update(TDto source)
        {
            TEntity entity = this.EntityToDtoMapping.Map(source);
            var attachment = this.DbSet.Find(source.Id);

            // Activity does not exist in database and it's new one
            if (attachment == null)
            {
                this.DbSet.Add(entity);
                return entity;
            }

            // Activity already exist in database and modify it
            Context.Entry(attachment).CurrentValues.SetValues(entity);
            Context.Entry(attachment).State = EntityState.Modified;
            this.Context.SaveChanges();
            return entity;
        }

        public virtual void Delete(object id)
        {

            var entity = this.DbSet.Find(id);
            var objectState = entity as IObjectState;
            if (objectState != null)
                objectState.State = ObjectState.Deleted;
            this.Delete((TEntity)entity);
            this.Context.SaveChanges();

        }

        public virtual void Delete(TDto source)
        {
            TEntity entity = this.EntityToDtoMapping.Map(source);
            this.DbSet.Attach(entity);
            this.DbSet.Remove(entity);
            this.Context.SaveChanges();

        }

        public virtual TEntity Insert(TDto source)
        {
            TEntity entity = this.EntityToDtoMapping.Map(source);
            this.DbSet.Add(entity);
            this.Context.SaveChanges();

            return entity;
        }

        public virtual IRepositoryQuery<TEntity, TDto> Query()
        {
            RepositoryQuery<TEntity, TDto> repositoryGetFluentHelper =
                new RepositoryQuery<TEntity, TDto>(this, this.EntityToDtoMapping);

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
            IQueryable<TEntity> query = this.DbSet;

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
