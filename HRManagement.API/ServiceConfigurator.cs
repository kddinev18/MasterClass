using HRManagement.DAL.Data;
using HRManagement.DAL.Repositories.Base;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HRManagement.API
{
    public static class ServiceConfigurator
    {
        public static void AddContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<HrManagementContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
    }
}