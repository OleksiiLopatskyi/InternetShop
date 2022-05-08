using InternetShop.BAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Contracts
{
    public interface IFileStorageClient<T> where T : Result
    {
        Task<T> SendAsync(HttpClient httpClient, MultipartFormDataContent form);
        Task<T> UploadAsync(IFormFileCollection files);
    }
}
