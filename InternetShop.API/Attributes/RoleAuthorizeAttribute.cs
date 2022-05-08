using InternetShop.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
namespace InternetShop.API.Validation
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private Role _roleEnum;
        public Role Role
        {
            get { return _roleEnum; }
            set
            {
                _roleEnum = value;
                Roles = value.ToString();
            }
        }
    }
}
