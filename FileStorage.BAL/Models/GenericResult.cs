using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BAL.Models
{
    public class GenericResult<T>:Result
    {
        public T Data { get; set; }
    }
}
