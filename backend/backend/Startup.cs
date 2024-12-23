using backend.Infrastructure;
using backend.IServices;
using backend.Model;
using backend.RepositoryInterfaces;
using backend.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;

namespace backend
{
    public static class Startup
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped(typeof(IRepository<Test>), typeof(CrudDatabaseRepository<Test, AppDbContext>));
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped(typeof(IRepository<Course>), typeof(CrudDatabaseRepository<Course, AppDbContext>));
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILtiService, LtiService>();
           // services.AddScoped(typeof(IRepository<TestResult>), typeof(CrudDatabaseRepository<TestResult, AppDbContext>));
            services.AddScoped<ITestResultRepository, TestResultRepository>();
            services.AddScoped<ITestResultService, TestResultService>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
        }
    }
}
