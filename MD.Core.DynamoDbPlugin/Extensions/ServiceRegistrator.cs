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
            try
            {
                var awsConf = configuration.GetSection("AWS")
                    .Get<BasicAwsConfig>()
                    .GetAWSOptions();

                if (awsConf == null)
                {
                    var envConf = new BasicAwsConfig
                    {
                        Profile = configuration.GetValue<string>("AWS_PROFILE"),
                        AccessKey = configuration.GetValue<string>("AWS_ACCESS_KEY_ID"),
                        SecretKey = configuration.GetValue<string>("AWS_SECRET_ACCESS_KEY"),
                        Region = configuration.GetValue<string>("AWS_REGION")
                    };
                    awsConf = envConf.GetAWSOptions();
                }
                if (awsConf == null)
                    throw new System.Exception();

                services.AddDefaultAWSOptions(awsConf);
            }
            catch (System.Exception)
            {
                throw new System.Exception("No valid DynamoDb configuraion found");
            }
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
