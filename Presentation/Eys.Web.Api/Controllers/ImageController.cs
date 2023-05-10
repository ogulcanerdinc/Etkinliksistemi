using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Domain.Services.Services;
using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;

namespace Eys.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IFileService _fileService;
        private IConfiguration _config;

        public ImageController(IFileService fileService, IConfiguration config)
        {
            _fileService = fileService;
            _config = config;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadedImageViewModel fileModel)
        {

            var response = new ServiceResult<UploadedImage>();
            response=await _fileService.AddImage(fileModel.File);

            return Ok();
        }

        [HttpGet]
        [Route("public/{filename}")]
        public async Task<IActionResult> GetPublicImage(string filename)
        {


            var response = new ServiceResult<UploadedImage>();
            var fileServiceResponse = await _fileService.GetImageByFilename(filename);

            var imageStream = System.IO.File.OpenRead(_config["ImageStoragePath"] + fileServiceResponse.FileName);
            return File(imageStream, fileServiceResponse.ContentType);

        }

        [HttpGet]
        [Route("public/Id/{id}")]
        public async Task<IActionResult> GetPublicImage(Guid id)
        {
            var response = new ServiceResult<UploadedImage>();
            var fileServiceResponse =await _fileService.GetImageById(id);
            if (fileServiceResponse.Id!=Guid.Empty)
            {
                var imageStream = System.IO.File.OpenRead(_config["ImageStoragePath"] + fileServiceResponse.FileName);
                return File(imageStream, fileServiceResponse.ContentType);
            }
            return response.HttpGetResponse();
        }
        [Authorize]
        [HttpPost]
        [Route("public/Image/Delete/{id}")]
        public async Task<IActionResult> GetImageDelete(Guid id)
        {
            var response = new ServiceResult<bool>();
            var serviceResult = await _fileService.DeleteImage(id);
            response.Result = serviceResult.IsSuccess;
            return response.HttpPostResponse();
        }
    }
}
