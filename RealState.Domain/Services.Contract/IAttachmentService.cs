using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Services.Contract
{
    public interface IAttachmentService
    {

        Task<string?> UploadAsync(IFormFile formFile, string folderName);

        bool Delete(string filePath);

    }
}
