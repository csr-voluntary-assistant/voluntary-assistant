using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Voluntariat.Services
{
    public class SecureCloudFileManager : ISecureCloudFileManager
    {
        private readonly string storageConnectionString;
        private readonly IWebHostEnvironment environment;
        private const string containerName = "all-files";

        public SecureCloudFileManager(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.storageConnectionString = configuration["ConnectionStrings:StorageAccount"];
            this.environment = environment;
        }

        public async Task<string> UploadFileAsync(string localPath)
        {
            var path = Path.Combine(environment.WebRootPath, localPath);

            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
            BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerName);
            using FileStream uploadFileStream = File.OpenRead(path);
            var cloudFileName = Guid.NewGuid().ToString();
            Azure.Response<BlobContentInfo> result = await container.UploadBlobAsync(cloudFileName, uploadFileStream);
            uploadFileStream.Close();
            return cloudFileName;
        }

        public async Task<Stream> GetBlobStream(string cloudIdentifier)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
            BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerName);
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
