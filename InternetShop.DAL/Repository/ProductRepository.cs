using InternetShop.DAL.Contracts;
using InternetShop.DAL.DataContext;
using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;
using InternetShop.DAL.Extensions;
using InternetShop.DAL.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using InternetShop.DAL.QueryParams;

namespace InternetShop.DAL.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Product>> FindAllAsync(ProductSearchParameters searchParameters,
            SortingParameters sortingParameters, PaginationParameters pagingParameters)
        {
            IQueryable<Product> products = DataContext.Products;
            products.Sort(sortingParameters).Filter(searchParameters);
            return await PaginatedList<Product>
                .CreateAsync(products, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
       
        public async Task<Product> FindEntityAsync(Expression<Func<Product, bool>> expression,
            params ProductProperties[] includes)
        {
            IQueryable<Product> products = DataContext.Products;
            foreach (var item in includes)
            {
                switch (item)
                {
                    case ProductProperties.None:
                        products = DataContext.Products;
                        break;
                    case ProductProperties.Rating:
                        products = products.Include(p => p.Rating);
                        break;
                    case ProductProperties.Group:
                        products = products.Include(p => p.Group);
                        break;
                    case ProductProperties.Images:
                        products = products.Include(p => p.Images);
                        break;
                    case ProductProperties.Comments:
                        products = products.Include(p => p.Comments);
                        break;
                }
            }
            return await products.FirstOrDefaultAsync(expression);
        }
    }
}
