using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Domain.Services.Services;
using Eys.Infra.Data.Database;
using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Eys.Domain.Services.Impl.Services
{
    public class FileService: IFileService
    {


        private readonly EysBaseContext context;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public FileService(EysBaseContext context, IConfiguration config, IHostingEnvironment env)
        {
            this.context = context;
            _config = config;
            _env = env;
        }

        public async Task<ServiceResult<UploadedImage>> AddImage(IFormFile fileModel)
        {
            var response = new ServiceResult<UploadedImage>();
            response.IsSuccess = false;
            var ImageList = new List<UploadedImage>();
                var newFileName = Guid.NewGuid().ToString("N") + "." + Path.GetExtension(fileModel.FileName);

                var fileProvider = new FileExtensionContentTypeProvider();
                fileProvider.TryGetContentType(newFileName, out string contentType);
                var addModel = (new Infra.Data.Entity.UploadedImage());
                var path = _config["ImageStoragePath"] + newFileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileModel.CopyToAsync(stream);
                    addModel = (new UploadedImage()
                    {
                        FileName = newFileName,
                        OriginalFileName = fileModel.FileName,
                        ContentType = contentType,
                        IsNeedAuth = false,
                        ReferanceFunction = "",
                        ReferanceId = ""
                    });
                    await context.UploadedImage.AddAsync(addModel);
                    response.Result = addModel;               
                }

            
            var ServiceResult = await context.SaveChangesAsync();
            if (ServiceResult > 0)
            {               
                response.IsSuccess = true;
            }

            return response;
        }

        public async Task<ServiceResult<bool>> DeleteImage(Guid id)
        {
            var result = new ServiceResult<bool>();
            var repoResponse = await context.UploadedImage.FirstOrDefaultAsync(x => x.Id == id);

            result.IsSuccess = false;
            if (repoResponse != null)
            {
                repoResponse.IsActive = false;
                repoResponse.IsDeleted = true;
                context.UploadedImage.Update(repoResponse);
                var SaveResult = await context.SaveChangesAsync();
                if (SaveResult > 0)
                    result.IsSuccess = true;
                result.Message = "Silme İşlemi Başarılı.";
            }
            else
            {
                result.Message = ("Kayıt bulunamadı");
            }
            return result;
        }

        public async Task<UploadedImageViewModel> GetImageByFilename(string filename)
        {
            var model = new UploadedImageViewModel();
            var getImage = await context.UploadedImage.FirstOrDefaultAsync(x => x.FileName == filename);
            if (getImage != null)
            {
                model.FileName = getImage.FileName;
                model.OriginalFileName = getImage.OriginalFileName;
                model.ContentType = getImage.ContentType;
                model.IsNeedAuth = getImage.IsNeedAuth;
                model.ReferanceFunction = getImage.ReferanceFunction;
                model.ReferanceId = getImage.ReferanceId;
                model.Id = getImage.Id;

            }
            return model;
        }

        public async Task<UploadedImageViewModel> GetImageById(Guid id)
        {
            var model = new UploadedImageViewModel();
            var getImage = await context.UploadedImage.FirstOrDefaultAsync(x => x.Id == id);
            if (getImage != null)
            {
                model.FileName = getImage.FileName;
                model.OriginalFileName = getImage.OriginalFileName;
                model.ContentType = getImage.ContentType;
                model.IsNeedAuth = getImage.IsNeedAuth;
                model.ReferanceFunction = getImage.ReferanceFunction;
                model.ReferanceId = getImage.ReferanceId;
                model.Id = getImage.Id;

            }
            return model;
        }
    }
}
