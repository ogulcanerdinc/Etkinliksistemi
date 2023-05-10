using Eys.Domain.Services.Impl.Helper;
using Eys.Domain.Services.Impl.Services;
using Eys.Domain.Services.Services;
using Eys.Infra.CrossCutting.AppUserIdentity.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICityService, CityService>();
			services.AddScoped<IAppUserAccountService, AppUserAccountService>();
			services.AddScoped<IEventTicketsService, EventTicketsService>();
            services.AddScoped<ITokenService, TokenService>();
        }
        public static void RegisterFileHelper(IServiceCollection services, string FileServerUrl)
        {
            services.AddScoped<ImageHelper>(
                s => new ImageHelper(s.GetRequiredService<IFileService>(),
                FileServerUrl));
        }
    }
}
