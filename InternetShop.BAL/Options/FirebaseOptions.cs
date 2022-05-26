using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace InternetShop.BAL.Options
{
    public  class FirebaseOptions
    {
        public const string FileStorageAPI = "FireBase";
        public string ApiKey { get; set; }
        public string Bucket { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
