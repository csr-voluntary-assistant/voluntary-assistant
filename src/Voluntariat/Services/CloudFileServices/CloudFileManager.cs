using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Voluntariat.Services.CloudFileServices
{
    public abstract class CloudFileManager : ICloudFileManager
    {
        private readonly string storageConnectionString;

        private readonly IWebHostEnvironment environment;

        protected abstract string ContainerName { get; }
        protected BlobServiceClient BlobServiceClient;

        public CloudFileManager(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.storageConnectionString = configuration["ConnectionStrings:StorageAccount"];
            this.environment = environment;
            BlobServiceClient = new BlobServiceClient(storageConnectionString);
        }

        public virtual async Task<string> UploadFileAsync(string localPath)
        {
            string path = GetPath(localPath);

            BlobContainerClient container = BlobServiceClient.GetBlobContainerClient(ContainerName);
            using FileStream uploadFileStream = File.OpenRead(path);
            var cloudFileName = $"{Guid.NewGuid()}{Path.GetExtension(localPath)}";
            Azure.Response<BlobContentInfo> result = await container.UploadBlobAsync(cloudFileName, uploadFileStream);
            uploadFileStream.Close();
            return cloudFileName;
        }

        private string GetPath(string localPath)
        {
            return Path.Combine(environment.WebRootPath, localPath);
        }

        public async Task<Stream> GetBlobStream(string cloudIdentifier)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
            BlobContainerClient container = blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient = container.GetBlobClient(cloudIdentifier);
            var result = await blobClient.DownloadAsync();
            return result.Value.Content;
        }

        public async Task DownloadToLocalFile(string cloudIdentifier, string localPath)
        {
            var path = Path.Combine(environment.WebRootPath, localPath);

            var stream = await GetBlobStream(cloudIdentifier);
            using FileStream downloadFileStream = File.OpenWrite(path);
            await stream.CopyToAsync(downloadFileStream);
            downloadFileStream.Close();
            stream.Close();
        }
    }
}
