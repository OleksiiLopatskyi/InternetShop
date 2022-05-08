using InternetShop.BAL.Contracts;
using InternetShop.BAL.Models;
using InternetShop.BAL.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace InternetShop.BAL.Services.FileStorageService
{
    public class ImageUploader : IImageUploader
    {
        private IHttpClientFactory _httpClientFactory;
        private FileStorageOptions _storageOptions;
        public ImageUploader(IHttpClientFactory httpClientFactory, 
            IOptions<FileStorageOptions> storageOptions)
        {
            _httpClientFactory = httpClientFactory;
            _storageOptions = storageOptions.Value;
        }
        private async Task<MultipartFormDataContent> CreateFormAsync(IFormFileCollection files)
        {
            var form = new MultipartFormDataContent();
            foreach (var file in files)
            {
                byte[] fileBytes = null;
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
                form.Add(new ByteArrayContent(fileBytes), "image", file.FileName);
            }
            return form;
        }
        public async Task<Result<IEnumerable<string>>> UploadAsync(IFormFileCollection files)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("FileStorage");
                var form = await CreateFormAsync(files);
                var response = await SendAsync(client, form);
                return response;
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<string>>
                {
                    Message = ex.Message,
                    StatusCode = Models.StatusCodes.InternalServerError
                };
            }
         
        }
        public async Task<Result<IEnumerable<string>>> SendAsync(HttpClient client,
            MultipartFormDataContent form)
        {
            var requestUrl = client.BaseAddress + _storageOptions.ImageUpload;
            var result = await client.PostAsync(requestUrl, form);
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Result<IEnumerable<string>>>(content);
        }
    }
}