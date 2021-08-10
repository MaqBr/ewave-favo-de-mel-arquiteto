using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class FileStorageServiceConfiguration
    {
        public FileStorageServiceConfiguration(string baseUrlAddress, string bucket, string accessKey, string secretKey, bool forceSsl)
        {
            BaseUrlAddress = baseUrlAddress;
            Bucket = bucket;
            AccessKey = accessKey;
            SecretKey = secretKey;
            ForceSsl = forceSsl;
        }

        public FileStorageServiceConfiguration()
        {

        }

        public FileStorageServiceConfiguration(IConfiguration configuration)
        {
            BaseUrlAddress = configuration["MinioSettings:Endpoint"];
            Bucket = configuration["MinioSettings:Bucket"];
            AccessKey = configuration["MinioSettings:AccessKey"];
            SecretKey = configuration["MinioSettings:SecretKey"];
            ForceSsl = Convert.ToBoolean(configuration["MinioSettings:ForceSsl"]);
            PathBase = configuration["MinioSettings:PathBase"];
        }

        public string AccessKey { get; set; }
        public string BaseUrlAddress { get; set; }
        public string Bucket { get; set; }
        public string PathBase { get; set; }
        public bool ForceSsl { get; set; }
        public string SecretKey { get; set; }
        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine($"____{nameof(FileStorageServiceConfiguration)}____");
            strB.AppendLine($" {nameof(BaseUrlAddress)}: {BaseUrlAddress}");
            strB.AppendLine(" ");
            strB.AppendLine($" {nameof(AccessKey)}: {AccessKey}");
            strB.AppendLine(" ");
            strB.AppendLine($" {nameof(SecretKey)}: {SecretKey}");
            strB.AppendLine(" ");
            strB.AppendLine($" {nameof(ForceSsl)}: {ForceSsl}");
            strB.AppendLine(" ");
            strB.AppendLine($" {nameof(Bucket)}: {Bucket}");
            strB.AppendLine(" ");
            strB.AppendLine($" {nameof(PathBase)}: {PathBase}");
            strB.AppendLine(" ");
            strB.AppendLine("________________________________________");
            return strB.ToString();
        }

    }
}
