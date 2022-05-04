using Azure.Storage.Blobs;

namespace StudentExample.Services
{
    public interface IAzureService
    {
        public T GetBlobData<T>(string blobName);
        int UploadData(string jsonContent, string blobName);
        IEnumerable<BlobClient> GetAllBlobs();
    }
}
