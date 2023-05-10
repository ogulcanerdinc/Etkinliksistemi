

using Eys.Infra.CrossCutting.AppUserIdentity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Data
{
	public class AppUserDbContext : IdentityDbContext<AppUser>
	{
		private readonly IHostingEnvironment _env;

		public AppUserDbContext(
			DbContextOptions<AppUserDbContext> options,
			IHostingEnvironment env) : base(options)
		{
			_env = env;
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// get the configuration from the app settings
			var config = new ConfigurationBuilder()
			   .SetBasePath(_env.ContentRootPath)
			   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			   .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
			   .Build();

			// define the database to use
			optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AppUser>().ToTable("AppUser");
			builder.Entity<IdentityRole>().ToTable("AppRole");
			builder.Entity<IdentityUserRole<string>>().ToTable("AppUserRole");
			builder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaim");
			builder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogin");
			builder.Entity<IdentityRoleClaim<string>>().ToTable("AppRoleClaim");
			builder.Entity<IdentityUserToken<string>>().ToTable("AppUserToken");


		}
	}
}
