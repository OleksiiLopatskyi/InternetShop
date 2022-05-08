using FileService.BAL.Contracts;
using FileStorage.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FilesController : CustomControllerBase
    {
        private readonly IFileUploader _fileUploader;
        public FilesController(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormCollection form)
        {
            var result = await _fileUploader.UploadFilesAsync(form.Files);
            return CustomResult(result);
        }
    }
}
