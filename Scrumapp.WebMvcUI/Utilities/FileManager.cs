using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Scrumapp.WebMvcUI.Utilities
{
    public class FileManager:IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileManager(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> Save(IFormFile formFile, string folder)
        {
            if (formFile == null)
            {
                return "";
            }

            if (formFile.Length <= 0)
            {
                return "";
            }

            var filePath = Path.GetFileName(formFile.FileName);
            var fileExtension = Path.GetExtension(filePath);
            var fileName = $"{Guid.NewGuid().ToString()}{fileExtension}";
            var fileSavePath = Path.Combine(_hostingEnvironment.WebRootPath, folder,fileName);

            using (var stream =new FileStream(fileSavePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return $"/{folder}/{fileName}";
        }

        public void Delete(string filePath)
        {
            var fileFullPath = $"{_hostingEnvironment.WebRootPath}/{filePath}";
            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
            }
        }
    }
}
