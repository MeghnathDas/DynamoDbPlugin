﻿/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.Core.DynamoDb
{
    using Amazon;
    using Amazon.Extensions.NETCore.Setup;
    internal class BasicAwsConfig
    {
        public string Profile { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; }
        public AWSOptions GetAWSOptions() {
            if (string.IsNullOrWhiteSpace(this.AccessKey) || string.IsNullOrWhiteSpace(this.SecretKey))
                return null;
            else
                return new AWSOptions()
                {
                    Profile = this.Profile,
                    Credentials = new Amazon.Runtime.BasicAWSCredentials(this.AccessKey, this.SecretKey),
                    Region = RegionEndpoint.GetBySystemName(this.Region)
                };
        }
    }
}
