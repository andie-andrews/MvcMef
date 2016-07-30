namespace MvcMef.Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using MvcMef.Dependencies;
    using Dependencies.Models;
    public sealed class RepositoryQuery<TEntity, TDto> : IRepositoryQuery<TEntity, TDto>
        where TEntity : class where TDto : class, IIdentity
    {
        private readonly List<Expression<Func<TEntity, object>>>
            _includeProperties;

        private readonly Repository<TEntity,TDto> _repository;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> _orderByQuerable;
        private int? _page;
        private int? _pageSize;

        private IMapping<TEntity, TDto> _mapping;

        public RepositoryQuery(Repository<TEntity,TDto> repository,IMapping<TEntity,TDto> mapping)
        {
            this._repository = repository;
            this._includeProperties =
                new List<Expression<Func<TEntity, object>>>();
            this._mapping = mapping;
        }

        public IRepositoryQuery<TEntity, TDto> Filter(
            Expression<Func<TEntity, bool>> filter)
        {
            this._filter = filter;
            return this;
        }

        public IRepositoryQuery<TEntity, TDto> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            this._orderByQuerable = orderBy;
            return this;
        }

        public IRepositoryQuery<TEntity, TDto> Include(
            Expression<Func<TEntity, object>> expression)
        {
            this._includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<TDto> GetPage(
            int page, int pageSize, out int totalCount)
        {
            this._page = page;
            this._pageSize = pageSize;
            totalCount = this._repository.Get(this._filter).Count();

            return this._mapping.Map(this._repository.Get(this._filter,
                this._orderByQuerable, this._includeProperties, this._page, this._pageSize));
        }

        public IEnumerable<TDto> Get()
        {
            return  this._mapping.Map(this._repository.Get(
                this._filter,
                this._orderByQuerable, this._includeProperties, this._page, this._pageSize));
        }
    }

}
