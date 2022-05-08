using InternetShop.DAL.Contracts;
using InternetShop.DAL.DataContext;
using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using InternetShop.DAL.Pagination;
using InternetShop.DAL.Extensions;

namespace InternetShop.DAL.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<Order>> FindAllAsync(OrderSearchParameters searchParameters,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters)
        {
            IQueryable<Order> query = DataContext.Set<Order>();
            query.Sort(sortingParameters).Filter(searchParameters);
            return await PaginatedList<Order>.CreateAsync(query,
                pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public override async Task<Order> FindEntityAsync(Expression<Func<Order, bool>> expression)
        {
            return await DataContext.Set<Order>().Include(i => i.Details).FirstOrDefaultAsync(expression);
        }
    }
}
