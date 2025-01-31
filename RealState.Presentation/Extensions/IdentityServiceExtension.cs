using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using RealState.Application.Common;
using RealState.Domain.Entities.Identity;
using RealState.Domain.Services.Contract;
using RealState.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;

namespace RealState.Presentation.Extensions
{
    public static class IdentityServiceExtension
    {

        public static IServiceCollection AddIdentityServicesExtension(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //TO DO
                };
            });

            return services;
        }
    }
}
