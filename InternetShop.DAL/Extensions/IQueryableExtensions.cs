using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;
using System.Linq.Expressions;
using System.Reflection;
using OrderByEnum = InternetShop.DAL.QueryParams.OrderBy;
namespace InternetShop.DAL.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, SortingParameters sortingParameters)
        {
            IQueryable<T> sortedQuery = null;
            PropertyInfo property = null;
            if (sortingParameters.SortBy != null)
            {
                property = typeof(Product).GetProperty(sortingParameters.SortBy);
            }
            if (property == null)
            {
                return query;
            }
            switch (sortingParameters.OrderBy)
            {
                case OrderByEnum.Desc:
                    sortedQuery = query.OrderByDescending(property.Name);
                    break;
                default:
                    sortedQuery = query.OrderBy(property.Name);
                    break;
            }
            return sortedQuery;
        }

        public static IQueryable<Product> Filter(this IQueryable<Product> query,
            ProductSearchParameters productSearchParameters)
        {

            if (productSearchParameters.Sizes != null)
            {
                string[] sizes = productSearchParameters.Sizes.Split(",");
                query = query.Where(p => sizes.Contains(p.Size.ToString()));
            }
            if (productSearchParameters.Search != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(productSearchParameters.Search.ToLower()));
            }
            if (productSearchParameters.Seasons != null)
            {
                string[] seasons = productSearchParameters.Seasons.Split(",");
                query = query.Where(p => seasons.Contains(p.Season));
            }
            if (productSearchParameters.Brands != null)
            {
                string[] brands = productSearchParameters.Brands.Split(",");
                query = query.Where(p => brands.Contains(p.Brand));
            }
            
            return query;
        }

        public static IQueryable<Order> Filter(this IQueryable<Order> query, OrderSearchParameters searchParameters)
        {
            if (searchParameters.Id != null)
            {
                query = query.Where(o => o.Id == searchParameters.Id);
            }

            if (searchParameters.ReceiverName != null)
            {
                query = query.Where(o => o.ReceiverName.Contains(searchParameters.ReceiverName));
            }

            if (searchParameters.Date != null && searchParameters.ReceiverName == null)
            {
                query = query.Where(o => o.Date == searchParameters.Date);
            }
            return query;
        }

    }
}
