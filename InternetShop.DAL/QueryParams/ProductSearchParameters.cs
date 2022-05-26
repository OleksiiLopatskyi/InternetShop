using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.QueryParams
{
    public class ProductSearchParameters
    {
        public string? Sizes { get; set; }
        public string? Seasons { get; set; }
        public string? Brands { get; set; }
        public string? Search { get; set; }
    }
}
