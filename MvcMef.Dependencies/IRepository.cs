using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
namespace MvcMef.Dependencies
{

    public interface IRepository<TEntity,TDto> where TEntity : class where TDto : class
    {
        TDto FindById(object id);
        TEntity InsertGraph(TDto entity);
        TEntity Update(TDto entity);
        void Delete(object id);
        void Delete(TDto entity);
        TEntity Insert(TDto entity);
        IRepositoryQuery<TEntity, TDto> Query();

    }

}
