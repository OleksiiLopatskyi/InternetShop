using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.QueryParams
{
    public enum OrderBy
    {
        Asc,
        Desc
    }
    public class SortingParameters
    { 
        public string? SortBy { get; set; }
        public OrderBy? OrderBy { get; set; }
    }
}
