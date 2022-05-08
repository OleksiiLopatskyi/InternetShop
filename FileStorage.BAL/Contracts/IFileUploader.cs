using FileStorage.BAL.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileService.BAL.Contracts
{
    public interface IFileUploader
    {
        ISaveConfiguration SaveConfiguration { get; }
        Task<Result> UploadFilesAsync(IFormFileCollection file);

    }
}
