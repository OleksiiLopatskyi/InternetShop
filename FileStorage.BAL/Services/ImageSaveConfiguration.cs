using FileService.BAL.Contracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.BAL.Services
{
    public class ImageSaveConfiguration : SaveConfiguration, ISaveConfiguration
    {
        public ImageSaveConfiguration(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public override string Path => _hostingEnvironment.WebRootPath;
        public override string FolderName => "ProductImages";
        public override string GenerateFileName(string fileName)
        {
           return Guid.NewGuid().ToString()+fileName;
        }
    }
}
