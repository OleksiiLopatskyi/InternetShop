using FileService.BAL.Contracts;
using FileStorage.BAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.BAL.Services
{
    public class ImageUploader : IFileUploader
    {
        private readonly ISaveConfiguration _saveConfiguration;
        public ImageUploader(ISaveConfiguration saveConfiguration)
        {
            _saveConfiguration = saveConfiguration;
        }
        public ISaveConfiguration SaveConfiguration => _saveConfiguration;

        public async Task<Result> UploadFilesAsync(IFormFileCollection images)
        {
            var urls = new List<string>();
            try
            {
                foreach (var image in images)
                {
                    var fileName = _saveConfiguration.GenerateFileName(image.FileName);
                    var path = $@"{_saveConfiguration.Path}\{_saveConfiguration.FolderName}\{fileName}";
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    urls.Add(path);
                }
                return new GenericResult<IEnumerable<string>> { Data = urls };
            }
            catch (Exception ex)
            {
                return new Result { Message = ex.Message, StatusCode = StatusCode.InternalServerError };
            }
        }
    }
}
