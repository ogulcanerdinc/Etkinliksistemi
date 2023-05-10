using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Services
{
    public interface IFileService
    {
        Task<ServiceResult<UploadedImage>> AddImage(IFormFile image);
        Task<UploadedImageViewModel> GetImageById(Guid id);
        Task<UploadedImageViewModel> GetImageByFilename(string filename);
        Task<ServiceResult<bool>> DeleteImage(Guid id);
    }
}
