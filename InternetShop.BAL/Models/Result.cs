using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetShop.BAL.Models
{
    public class Result<T> : Result
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
    public class Result
    {
        public StatusCodes StatusCode { get; set; }
        public string Message { get; set; }
    }
}
