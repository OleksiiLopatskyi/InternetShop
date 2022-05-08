using InternetShop.DAL.Contracts;
using InternetShop.DAL.DataContext;
using InternetShop.DAL.Entities;


namespace InternetShop.DAL.Repository
{
    public class GroupRepository:RepositoryBase<Group>,IGroupRepository
    {
        public GroupRepository(DatabaseContext databaseContext):base(databaseContext)
        {
        }
    }
}
