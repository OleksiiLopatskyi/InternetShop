
using InternetShop.DAL.QueryParams;

namespace InternetShop.DAL.Contracts
{
    public interface ISearchable<TEntity, TSearchParameter>
    {
        public Task<IEnumerable<TEntity>> FindAllAsync(TSearchParameter searchParameter,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters);
    }
}
