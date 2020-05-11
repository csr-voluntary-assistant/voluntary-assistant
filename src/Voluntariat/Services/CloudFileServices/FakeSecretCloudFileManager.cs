using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace Voluntariat.Services.CloudFileServices
{
    public class FakeCloudFileManager : ISecureCloudFileManager, IPublicCloudFileManager
    {
        private IWebHostEnvironment environment;

        public FakeCloudFileManager(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public Task DownloadToLocalFile(string cloudIdentifier, string localPath)
        {
            var sourcePath = GetLocalPath(localPath);
            if (cloudIdentifier != sourcePath)
                File.Copy(cloudIdentifier, sourcePath);

            return Task.CompletedTask;
        }

        public Task<Stream> GetBlobStream(string cloudPath)
        {
            return Task.FromResult((Stream)File.OpenRead(cloudPath));
        }

        public Task<string> UploadFileAsync(string localPath)
        {
            var path = GetLocalPath(localPath);
            var cloudPath = GetCloudPath(localPath);
            File.Copy(path, cloudPath);
            return Task.FromResult(cloudPath);
        }

        private string GetCloudPath(string localPath)
        {
            return Path.Combine(environment.WebRootPath, $"{System.Guid.NewGuid()}{Path.GetExtension(localPath)}");
        }

        private string GetLocalPath(string localPath)
        {
            return Path.Combine(environment.WebRootPath, localPath);
        }
    }
}
