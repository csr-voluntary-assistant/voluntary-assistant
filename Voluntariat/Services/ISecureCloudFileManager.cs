using System.IO;
using System.Threading.Tasks;

namespace Voluntariat.Services
{
    public interface ISecureCloudFileManager
    {
        Task DownloadToLocalFile(string cloudIdentifier, string localPath);
        Task<Stream> GetBlobStream(string cloudIdentifier);
        Task<string> UploadFileAsync(string localPath);
    }
}