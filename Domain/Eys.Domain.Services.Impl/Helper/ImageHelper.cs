using Eys.Domain.Services.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Impl.Helper
{
    public class ImageHelper
    {
        private readonly IFileService fileService;
        private readonly string FileServerUrl;

        public ImageHelper(IFileService fileService, string fileServerUrl)
        {
            this.fileService = fileService;
            FileServerUrl = fileServerUrl;
        }


        public string GetImageUrlByFilename(string filename)
        {
            return FileServerUrl + "/image/public/" + filename;
        }

        public string GetImageUrlById(Guid id)
        {
            var fileResponse = fileService.GetImageById(id);
            if (fileResponse.Id!=null)
            {
                return this.GetImageUrlByFilename(fileResponse.Result.FileName);
            }
            return "";
        }



    }
}
