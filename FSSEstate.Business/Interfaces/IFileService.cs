using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace FSSEstate.Business.Interfaces
{
    public interface IFileService
    {
        public Task<string> UploadImageAsync(IFormFile image, string rootpath);
        public Task<bool> DeleteImageAsync(string subpath);
        public Task<byte[]> GetImageAsync(string subpath);
    }
}
