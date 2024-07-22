using FSSEstate.Business.Implementations.Helpers;
using FSSEstate.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Hosting;

namespace FSSEstate.Business.Implementations
{
    public class FileService : IFileService
    {

        private readonly string ROOTPATH;

        public FileService(IHostEnvironment env)
        {
            ROOTPATH = env.ContentRootPath;
        }

        public async Task<bool> DeleteImageAsync(string subpath)
        {
            string path = Path.Combine(ROOTPATH, subpath);

            if (File.Exists(path))
            {
                File.Delete(path);
                await Task.CompletedTask;

                return true;
            }

            return false;
        }

        public async Task<byte[]> GetImageAsync(string subpath)
        {
            string path = Path.Combine(ROOTPATH, subpath);

            if (File.Exists(path))
            {
                byte[] fileBytes;

                // Read file asynchronously
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    fileBytes = new byte[fileStream.Length];
                    fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);
                }

                return fileBytes;
            }

            throw new FileNotFoundException(subpath);
        }

        public async Task<string> UploadImageAsync(IFormFile image, string rootpath)
        {
            string newImageName = MediaHelper.MakeImageName(image.FileName);
            string subPath = Path.Combine("Media", "Images", rootpath, newImageName);
            string path = Path.Combine(ROOTPATH, subPath);

            // Ensure that the directory exists before creating the file
            string directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                // If it doesn't exist, create it
                Directory.CreateDirectory(directoryPath);
            }

            var stream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(stream);
            stream.Close();

            return subPath;
        }
    }
}
