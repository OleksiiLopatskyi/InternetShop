
using InternetShop.DAL.Pagination;
using InternetShop.DAL.QueryParams;

namespace InternetShop.DAL.Contracts
{
    public interface ISearchable<TEntity, TSearchParameter>
    {
        public Task<PaginatedList<TEntity>> FindAllAsync(TSearchParameter searchParameter,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters);
    }
}
