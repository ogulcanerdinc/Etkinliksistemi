using Eys.Domain.Models.Base;
using Eys.Infra.CrossCutting.AppUserIdentity.Model;
using Eys.Infra.CrossCutting.AppUserIdentity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eys.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAppUserAccountService _appUserAccountService;
        private ITokenService tokenService;
        private readonly IConfiguration _config;
        public AuthController(IAppUserAccountService appUserAccountService, ITokenService tokenService, IConfiguration config)
        {
            _appUserAccountService = appUserAccountService;
            this.tokenService = tokenService;
            _config = config;
        }


        [HttpPost("Login")]
        [SwaggerResponse(200, "Token Bilgileri", typeof(UserTokenModel))]
        [SwaggerResponse(400, "Hatalı Email/Şifre Bilgileri")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {

            var response = new ServiceResult<UserTokenModel>();
            var serviceResponse = await _appUserAccountService.PasswordSignIn(loginRequest);
            if (serviceResponse.IsSuccess)
            {
                var tokenServiceResponse = tokenService.GenerateUserToken(serviceResponse.Result, _config["Jwt:Key"], _config["Jwt:Issuer"]);
                if (tokenServiceResponse.Result==null)
                {
                    return serviceResponse.HttpPostResponse();
                }
                response.Result = tokenServiceResponse.Result;

            }

            return response.HttpPostResponse();
        }
        [HttpPost("SignUp")]
        [SwaggerResponse(200, "Kayıt Başarılı")]
        [SwaggerResponse(400, "Email Zaten Kayıtlı")]
        public async Task<IActionResult> SignUp(SignUpRequestModel model)
        {
            var response = await _appUserAccountService.UserSignUp(model);

            return response.HttpPostResponse();
        }
    }
}
