using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.QueryParams
{
    public class ProductSearchParameters
    {
        public string? Name { get; set; }
        public decimal? PriceStartRange { get; set; }
        public decimal? PriceEndRange { get; set; }
    }
}
