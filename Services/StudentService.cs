using Azure.Storage.Blobs;
using StudentExample.Entities;

namespace StudentExample.Services
{
    public class StudentService : IStudentService
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;


        // Blob service client 
        private BlobServiceClient blobServiceClient;

        // Blob container client
        private BlobContainerClient container;

        // Container name
        private string ContainerName;

        public StudentService(IConfiguration Configuration)
        {
            this.Configuration = Configuration;

            // Azure Storage init
            AzureStorageBlobInit();
        }

        public bool AddStudent(Student student)
        {
            return new();
        }

        public Student GetStudent(int id)
        {
            return new Student();
        }

        public List<Student> GetStudents()
        {
            return new List<Student>();
        }

        public bool RemoveStudent(int id)
        {
            return true;
        }

        public Student UpdateStudent(int id, Student student)
        {
            return student;
        }

        #region Azure Storage Blob

        private void AzureStorageBlobInit()
        {
            // Create BlobServiceClient from the connection string.
            blobServiceClient = new BlobServiceClient(Configuration["ConnectionString"]);

            // Get and create the container for the blobs
            container = blobServiceClient.GetBlobContainerClient(Configuration["ContainerName"]);
        }

        // Uploading Json Text
        private async Task UploadStudentToBlob()
        {
            string jsonEntityContent = "{ }";

            BlobClient blob = container.GetBlobClient("BlobName");
            await blob.UploadAsync(new MemoryStream(Encoding.UTF8.GetBytes(jsonEntityContent)),
                    new BlobHttpHeaders()
                    {
                        ContentType = "application/json"
                    });
        }
        #endregion

    }

}
