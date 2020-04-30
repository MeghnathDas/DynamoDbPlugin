/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.DemoWebAppWithDynamoDb.DataAccess
{
    using MD.Core.DynamoDb.Extensions.DependencyInjection;
    using MD.DemoWebAppWithDynamoDb.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public static class DataAccessServiceRegistrator
    {
        public static IServiceCollection AddDataAccessServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDynamoDbServices(configuration);
            services.AddSingleton<AppDbContext>();
            services.AddTransient<IRepo<NoteCategory>, CategoryRepo>();
            services.AddTransient<IRepo<Note>, NoteRepo>();

            return services;
        }
    }
}
