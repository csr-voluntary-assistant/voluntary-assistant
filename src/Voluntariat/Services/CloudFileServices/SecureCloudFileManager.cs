using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Voluntariat.Services.CloudFileServices
{
    public class SecureCloudFileManager : CloudFileManager, ISecureCloudFileManager
    {
        protected override string ContainerName => "all-files";

        public SecureCloudFileManager(IConfiguration configuration, IWebHostEnvironment environment): base(configuration, environment)
        {
            
        }
    }
}
