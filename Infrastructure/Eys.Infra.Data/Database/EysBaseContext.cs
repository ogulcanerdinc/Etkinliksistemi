using Eys.Infra.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.Data.Database
{
    public class EysBaseContext : DbContext
    {
        private readonly IHostingEnvironment _env;
        public EysBaseContext(
            DbContextOptions<EysBaseContext> options,
            IHostingEnvironment env) : base(options)
        {
            _env = env;
            this.Database.SetCommandTimeout(999999);
        }

        public DbSet<Events> Events { get; set; }
        public DbSet<EventImages> EventImages { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UploadedImage> UploadedImage { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<EventTickets> EventTickets { get; set; }











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

        public override int SaveChanges()
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckDeletable();
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void CheckDeletable()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntityWithDate && (x.State == EntityState.Deleted && ((BaseEntityWithDate)x.Entity).IsDeletable != true));

            if (entities != null && entities.Count() > 0)
            {
                throw new Exception("Seçilen veri silinemez");
            }
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntityWithDate && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntityWithDate)entity.Entity).DateCreated = DateTime.Now;
                }

                ((BaseEntityWithDate)entity.Entity).DateModified = DateTime.Now;
            }
        }

	}
}
