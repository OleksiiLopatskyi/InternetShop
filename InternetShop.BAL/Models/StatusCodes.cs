using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Models
{
    public enum StatusCodes
    {
        Success = 0,
        NotFound = 1,
        InternalServerError = 2,
        ValidationError = 3,
        BadRequest = 4
    }
}
