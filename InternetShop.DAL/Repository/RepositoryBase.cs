using InternetShop.DAL.Contracts;
using InternetShop.DAL.DataContext;
using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using InternetShop.DAL.Extensions;

namespace InternetShop.DAL.Repository
{
    public abstract class RepositoryBase<TEntity>
        : IRepositoryBase<TEntity> where TEntity : class
    {
        protected DatabaseContext DataContext;

        public RepositoryBase(DatabaseContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DataContext.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            DataContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            DataContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            var data = DataContext.Set<Order>();
            data.Include(i => i.Details);


           // return await DataContext.Set<TEntity>().AsNoTracking().ToListAsync();
            return await DataContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DataContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        public virtual async Task<TEntity> FindEntityAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DataContext.Set<TEntity>().FirstOrDefaultAsync(expression);
        }
    }
}
