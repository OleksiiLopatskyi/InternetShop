using InternetShop.DAL.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InternetShop.DAL.Contracts
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FindEntityAsync(Expression<Func<TEntity, bool>> expression);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
