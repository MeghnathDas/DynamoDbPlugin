/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.Core.DynamoDb.Extensions.DependencyInjection
{
    using Amazon.DynamoDBv2;
    using Amazon.Extensions.NETCore.Setup;
    using MD.Core.DynamoDb;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public static class DynamoDbServiceRegistrator
    {
        public static IServiceCollection AddDynamoDbServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(
                configuration.GetSection("AWS").Get<BasicAwsConfig>().GetAWSOptions()
                );
            services.AddAWSService<IAmazonDynamoDB>();

            return services;
        }
        public static IServiceCollection AddDynamoDbServices(
            this IServiceCollection services,
            AWSOptions awsConfig)
        {
            services.AddDefaultAWSOptions(awsConfig);
            services.AddAWSService<IAmazonDynamoDB>();

            return services;
        }
    }
}
