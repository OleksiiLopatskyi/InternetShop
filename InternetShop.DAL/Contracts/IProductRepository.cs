using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;
using InternetShop.DAL.QueryParams;
using System.Linq.Expressions;

namespace InternetShop.DAL.Contracts
{
    public interface IProductRepository:IRepositoryBase<Product>,
        ISearchable<Product, ProductSearchParameters>
    {
        Task<Product> FindEntityAsync(Expression<Func<Product, bool>> expression, 
            params ProductProperties[] includes);
    }
}
