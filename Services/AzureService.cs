using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using System.Text;

namespace StudentExample.Services
{
    public class AzureService : IAzureService
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration configuration;

        private BlobServiceClient blobServiceClient;
        private BlobContainerClient container;

        public AzureService(IConfiguration configuration)
        {
            this.configuration = configuration;

            blobServiceClient = new BlobServiceClient(configuration["ConnectionString"]);
            container = blobServiceClient.GetBlobContainerClient(configuration["ContainerName"]);
        }

        public T GetBlobData<T>(string blobName)
        {
            BlobClient blob = container.GetBlobClient(blobName);

            using (Stream s = blob.OpenRead())
            {
                using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        return serializer.Deserialize<T>(reader);
                    }
                }
            }
        }

        public int qUploadData(string jsonContent, string blobName)
        {
            BlobClient blob = container.GetBlobClient(blobName);

            var result = blob.Upload(new MemoryStream(Encoding.UTF8.GetBytes(jsonContent)),
                    new BlobHttpHeaders() { ContentType = "application/json" });

            return result.GetRawResponse().Status;
        }

        public int UploadData(string jsonContent, string blobName)
        {
            BlobClient blob = container.GetBlobClient(blobName);

            var result = blob.Upload(new MemoryStream(Encoding.UTF8.GetBytes(jsonContent)),
                new BlobHttpHeaders() { ContentType = "application/json" });

            return result.GetRawResponse().Status;
        }

        public IEnumerable<BlobClient> GetAllBlobs()
        {
            foreach (BlobItem blob in container.GetBlobs(BlobTraits.None, BlobStates.None, string.Empty))
            {
                yield return container.GetBlobClient(blob.Name);
            }
        }
    }
}
