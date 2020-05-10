using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Voluntariat.Services.CloudFileServices
{
    public class PublicCloudFileManager : CloudFileManager, IPublicCloudFileManager
    {
        public PublicCloudFileManager(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
            StorageAccount = BlobServiceClient.AccountName;
        }

        private string StorageAccount { get; set; }

        protected override string ContainerName => "public-images";

        public override async Task<string> UploadFileAsync(string localPath)
        {
            var result = await base.UploadFileAsync(localPath);

            return $"https://{StorageAccount}.blob.core.windows.net/{ContainerName}/{result}";
            
        }
    }
}
