using HRManagement.DAL.Data;
using HRManagement.DAL.Data.Contracts;
using HRManagement.DAL.Repositories.Base;
using HRManagement.Infrastructure.Contracts;
using System.Reflection;

namespace HRManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            AddServices(builder);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<HrManagementContext>();
            builder.Services.AddScoped<UnitOfWork>();

            foreach (Type interfaceType in FindDerivedInterfaces(typeof(IBaseService)))
            {
                Type serviceType = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(type => type.IsClass && !type.IsAbstract)
                    .Where(type => interfaceType.IsAssignableFrom(type))
                    .First();

                builder.Services.AddScoped(interfaceType, serviceType);
            }
        }

        public static IEnumerable<Type> FindDerivedInterfaces(Type baseInterface)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(type => type.IsInterface)
                .Where(type => type.GetInterfaces().Contains(baseInterface));
        }
    }
}