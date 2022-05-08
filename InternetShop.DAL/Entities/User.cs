using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;

namespace InternetShop.DAL.Entities
{
    public enum Role
    {
        Admin,
        User
    }
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
