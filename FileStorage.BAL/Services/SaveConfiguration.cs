using FileService.BAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Threading.Tasks;

namespace FileService.BAL.Services
{
    public abstract class SaveConfiguration : ISaveConfiguration
    {
        protected IHostingEnvironment _hostingEnvironment;
        public SaveConfiguration(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public abstract string Path { get; }
        public abstract string FolderName { get; }
        public abstract string GenerateFileName(string fileName);
    }
}
