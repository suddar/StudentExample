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
        BlobClient blob;


        public AzureService(IConfiguration configuration)
        {
            this.configuration = configuration;

            _ = InitClient();
        }
        private async         Task InitClient()
        {
            //blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=southeastasiastorage001;AccountKey=6TVjUp2lo17CdQco+pCG6tUvH8uPDlK/Zi8srsBvwxyJ9NZFnm8OnNqGkvX93DX0/fRPrSjJhqWA+AStPsWTag==;EndpointSuffix=core.windows.net");
            //container = blobServiceClient.GetBlobContainerClient("student");
            //blob = container.GetBlobClient("student-demo-1");// truyen text gi thi no tao ra blob nhu the
            blobServiceClient = new BlobServiceClient(configuration["ConnectionString"]);
            container = blobServiceClient.GetBlobContainerClient(configuration["ContainerName"]);
            blob = container.GetBlobClient(configuration["BlobName"]);
        }

        public T GetBlobData<T>(string blobName)
        {
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

            var result = blob.Upload(new MemoryStream(Encoding.UTF8.GetBytes(jsonContent)),
                    new BlobHttpHeaders() { ContentType = "application/json" });

            return result.GetRawResponse().Status;
        }

        public int UploadData(string jsonContent, string blobName)
        {

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
