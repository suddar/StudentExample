//using Azure.Storage.Blobs;
//using Azure.Storage.Blobs.Models;
//using Newtonsoft.Json;
//using System.Text;

//namespace StudentExample.Services
//{
//    public class AzureService
//    {
//        // requires using Microsoft.Extensions.Configuration;
//        private readonly IConfiguration Configuration;

//        // Blob service client 
//        private BlobServiceClient blobServiceClient;

//        // Blob container client
//        private BlobContainerClient container;

//        // Container name
//        private string ContainerName;

//        private void AzureService(Configuration configuration)
//        {

//            // Create BlobServiceClient from the connection string.
//            blobServiceClient = new BlobServiceClient(Configuration["ConnectionString"]);

//            // Get and create the container for the blobs
//            container = blobServiceClient.GetBlobContainerClient(Configuration["ContainerName"]);
//        }

//        // Uploading Json Text
//        public async Task<Entity> UploadEntityToBlobAsync<Entity>(BlobClient blobJson)
// where Entity : class, new()
//        {
//            {
//            string jsonEntityContent = JsonConvert.SerializeObject(student);

//            BlobClient blob = container.GetBlobClient(ContainerName);
//            await blob.UploadAsync(new MemoryStream(Encoding.UTF8.GetBytes(jsonEntityContent)),
//                new BlobHttpHeaders()
//                {
//                    ContentType = "application/json"
//                });
//        }

//        BlobClient blobJson = container.GetBlobClient("BlobName");
//        Entity jsonEntity = await GetEntityBlobAsync<Entity>(blobJson);

//        public async Task<Entity> GetEntityBlobAsync<Entity>(BlobClient blobJson)
//         where Entity : class, new()
//        {
//            try
//            {
//                using (MemoryStream s = new MemoryStream())
//                {
//                    await blobJson.DownloadToAsync(s);
//                    using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
//                    {
//                        using (JsonReader reader = new JsonTextReader(sr))
//                        {
//                            JsonSerializer serializer = new JsonSerializer();
//                            var serializer.Deserialize<Entity>(reader);
//                        }
//                    }
//                }
//            }
//            catch (RequestFailedException ex)
//                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
//            {
//                return null;
//            }
//        }
//    }
//}
