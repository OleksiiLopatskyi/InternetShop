using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BAL.Models
{
    public enum StatusCode
    {
        Success,
        InternalServerError,
        BadRequest
    }
    public class Result
    {
        public string Message { get; set; }
        public StatusCode StatusCode { get; set; }
    }
}
