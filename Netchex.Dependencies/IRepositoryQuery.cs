namespace Netchex.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepositoryQuery<TEntity, out TDto>
        where TEntity : class
    {
        IRepositoryQuery<TEntity, TDto> Filter(
            Expression<Func<TEntity, bool>> filter);

        IRepositoryQuery<TEntity, TDto> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        IRepositoryQuery<TEntity, TDto> Include(
            Expression<Func<TEntity, object>> expression);

        IEnumerable<TDto> GetPage(
            int page, int pageSize, out int totalCount);

        IEnumerable<TDto> Get();
    }
}