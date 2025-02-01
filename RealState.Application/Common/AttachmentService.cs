using Microsoft.AspNetCore.Http;
using RealState.Domain.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RealState.Application.Common
{
    public class AttachmentService : IAttachmentService
    {

        private List<string> allowedExtensions = new List<string>() { ".png", ".jpg", ".jpeg" };

        private int _allowedMaxSize = 2_097_152;

        public async Task<string?> UploadAsync(IFormFile formFile, string folderName)
        {
            var extension = Path.GetExtension(formFile.FileName);

            if (!allowedExtensions.Contains(extension))
                return null;

            if (formFile.Length > _allowedMaxSize)
                return null;
            
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filename = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(folderPath, filename);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            await formFile.CopyToAsync(fileStream);

            return filename;

        }

        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
