using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.QueryParams
{
    public class OrderSearchParameters
    {
        public int? Id { get; set; }
        public string? ReceiverName { get; set; }
        public DateTime? Date { get; set; }
    }
}
