using Firebase.Auth;
using Firebase.Storage;
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
        private FirebaseOptions _fireBaseOptions;
        public ImageUploader(IHttpClientFactory httpClientFactory, 
            IOptions<FirebaseOptions> storageOptions)
        {
            _httpClientFactory = httpClientFactory;
            _fireBaseOptions = storageOptions.Value;
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
        public async Task<Result<IEnumerable<string>>> UploadAsync(IEnumerable<IFormFile> files)
        {
            var urlCollection = new List<string>();
            var cancellationToken = new CancellationTokenSource();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_fireBaseOptions.ApiKey));
            try
            {
                var signIn = await auth
                .SignInWithEmailAndPasswordAsync(_fireBaseOptions.Email, _fireBaseOptions.Password);

                foreach (var file in files)
                {
                    var upload = new FirebaseStorage(_fireBaseOptions.Bucket, new FirebaseStorageOptions()
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(signIn.FirebaseToken),
                        ThrowOnCancel = true
                    }).Child(Guid.NewGuid().ToString());
                    using (var stream = file.OpenReadStream())
                    {
                        await upload.PutAsync(stream, cancellationToken.Token);
                    }
                    urlCollection.Add(await upload.GetDownloadUrlAsync());
                }
                return new Result<IEnumerable<string>> { Data = urlCollection };
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
       /* public async Task<Result<IEnumerable<string>>> SendAsync(HttpClient client,
            MultipartFormDataContent form)
        {
            var requestUrl = client.BaseAddress + _fireBaseOptions.ImageUpload;
            var result = await client.PostAsync(requestUrl, form);
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Result<IEnumerable<string>>>(content);
        }*/
    }
}