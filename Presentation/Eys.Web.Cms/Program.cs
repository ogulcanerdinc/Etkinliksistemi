using Eys.Infra.CrossCutting.IoC;
using System.Text.Json.Serialization;
using Eys.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Eys.Infra.CrossCutting.AppUserIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Eys.Infra.CrossCutting.AppUserIdentity.Entity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews().AddViewLocalization().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<EysBaseContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
builder.Services.AddDbContext<AppUserDbContext>(options =>
options.UseSqlServer(connectionString));
NativeInjectorBootStrapper.RegisterServices(builder.Services);
NativeInjectorBootStrapper.RegisterFileHelper(builder.Services, builder.Configuration["ImageServerUrl"]);
builder.Services
       .AddControllers()
       .AddJsonOptions(options =>
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
       );
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
	options.User.AllowedUserNameCharacters = null;
	options.Password.RequiredLength = 4;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
})
			   .AddRoles<IdentityRole>()
			   .AddEntityFrameworkStores<AppUserDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
