using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DTribe.Core.Services
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        FileStream GetFileStream(string filename);
    }
    public class StorageService : IStorageService
    {

 

        public FileStream GetFileStream(string filename)
        {
            //var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            //var filePath = Path.Combine(uploadsFolderPath, filename);

            //if (!System.IO.File.Exists(filePath))
            //{
            //    throw new FileNotFoundException("File not found.");
            //}

            //return new FileStream(filePath, FileMode.Open, FileAccess.Read);


            return null;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            //if (file == null || file.Length == 0)
            //    throw new ArgumentException("File is empty.");

            //var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            //if (!Directory.Exists(uploadsFolderPath))
            //{
            //    Directory.CreateDirectory(uploadsFolderPath);
            //}

            //var filePath = Path.Combine(uploadsFolderPath, file.FileName);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}

            //return $"/uploads/{file.FileName}";

            return null;
        }
    }
}
