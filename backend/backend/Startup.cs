using backend.Infrastructure;
using backend.IServices;
using backend.Model;
using backend.RepositoryInterfaces;
using backend.UseCases;

namespace backend
{
    public static class Startup
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<ITestRepository, TestRepository>();
        }
    }
}
