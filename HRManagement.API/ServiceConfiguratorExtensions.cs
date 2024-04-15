using HRManagement.DAL.Data;
using HRManagement.DAL.Repositories.Base;
using HRManagement.Domain.Constants;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HRManagement.API
{
    public static class ServiceConfiguratorExtensions
    {
        public static void AddContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<HrManagementContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("HrManagement")
                .AddEntityFrameworkStores<HrManagementContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration[Jwt.Issuer],
                    ValidAudience = builder.Configuration[Jwt.Audience],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[Jwt.Key]))
                };
            });
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UnitOfWork>();
            Type baseInterface = typeof(IBaseService);
            IEnumerable<Type> derivedInterfaces = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(type => type.IsInterface)
                .Where(type => type.GetInterfaces().Contains(baseInterface));

            foreach (Type interfaceType in derivedInterfaces)
            {
                Type serviceType = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(type => type.IsClass && !type.IsAbstract)
                    .Where(type => interfaceType.IsAssignableFrom(type))
                    .First();

                builder.Services.AddScoped(interfaceType, serviceType);
            }
        }

        public static void AddCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    config =>
                    {
                        config.WithOrigins("http://localhost:4200", "https://localhost:7184")
                            .AllowAnyHeader()
                            .WithMethods(HttpMethod.Get.Method, HttpMethod.Post.Method, HttpMethod.Put.Method,
                                HttpMethod.Delete.Method);
                    });
            });
        }
    }
}