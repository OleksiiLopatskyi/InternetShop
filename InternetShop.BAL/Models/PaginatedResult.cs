using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Models
{
    public class PaginatedResult<T> : Result
    {
        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
