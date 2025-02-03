using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealState.Application.Common;
using RealState.Application.Services;
using RealState.Domain;
using RealState.Domain.Entities.Identity;
using RealState.Domain.Repositories.Contract;
using RealState.Domain.Services.Contract;
using RealState.Infrastructure;
using RealState.Infrastructure.Data;
using RealState.Infrastructure.Identity;
using RealState.Presentation.Extensions;

namespace RealState.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentityServicesExtension(builder.Configuration);

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IAttachmentService, AttachmentService>();

            builder.Services.AddScoped<IVillaService, VillaService>();

            builder.Services.AddScoped<IVillaNumberService, VillaNumberService>();

            builder.Services.AddScoped<IAmenityService, AmenityService>();

            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<ApplicationDbContext>();

            var _IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();

                await ApplicationDbContextSeed.SeedAsync(_dbContext);

                await _IdentityDbContext.Database.MigrateAsync();

                var _userManager = services.GetRequiredService<UserManager<AppUser>>();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await AppIdentityDbContextSeed.SeedUserAsync(_userManager, roleManager);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error has been occured during apply the migration");
            }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
