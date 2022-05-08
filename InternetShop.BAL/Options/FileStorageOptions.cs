using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Options
{
    public  class FileStorageOptions
    {
        public const string FileStorageAPI = "FileStorageAPI";
        public string BaseUrl { get; set; }
        public string ImageUpload { get; set; }
    }
}
