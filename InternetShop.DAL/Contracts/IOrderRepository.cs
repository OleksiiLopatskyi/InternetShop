using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;
using InternetShop.DAL.Pagination;
using System.Linq.Expressions;

namespace InternetShop.DAL.Contracts
{
    public interface IOrderRepository:IRepositoryBase<Order>,ISearchable<Order, OrderSearchParameters>
    {
        public Task<PaginatedList<Order>> FindByConditionAsync(Expression<Func<Order, bool>> expression,
          PaginationParameters pagingParameters);
    }
}
