using System.IO;
using System.Threading.Tasks;

namespace Voluntariat.Services
{
    public interface ICloudFileManager
    {
        Task DownloadToLocalFile(string cloudIdentifier, string localPath);
        Task<Stream> GetBlobStream(string cloudIdentifier);
        Task<string> UploadFileAsync(string localPath);
    }
}