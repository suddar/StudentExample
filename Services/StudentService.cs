using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using StudentExample.Entities;
using System.Text;

namespace StudentExample.Services
{
    public class StudentService : IStudentService
    {
        private IAzureService azureService;
        private string blobName = string.Empty;
        private IConfiguration configuration;
        private List<Student> studentList;

        public StudentService(IAzureService azureService, IConfiguration configuration)
        {
            this.azureService = azureService;
            this.configuration = configuration;
            blobName = configuration["BlobName"];
        }

        public Student? AddStudent(Student student)
        {
            try
            {
                // get students data from azure blob storage
                var studentlist = RetrieveStudentListFromAzureStorage(blobName);
                if (studentlist == null) return null;

                // 
                student.Id = GenId();
                studentlist?.Add(student);

                // up load data to a blob
                var resultCode = azureService.UploadData(JsonConvert.SerializeObject(studentlist), blobName);
                if (resultCode == 201) return null;

                return student;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Student? GetStudent(int id)
        {
            // get students data from azure blob storage
            studentList = RetrieveStudentListFromAzureStorage(blobName);
            if (studentList == null) return null;

            var student = studentList.FirstOrDefault(student => student.Id == id);
            return student;
        }

        public List<Student> GetStudents()
        {
            return studentList;
        }

        public Student? UpdateStudent(int id, Student student)
        {
            try
            {
                // get students data from azure blob storage
                studentList = RetrieveStudentListFromAzureStorage(blobName);
                if (studentList == null) return null;

                // find student by id
                var validStudent = studentList.FirstOrDefault(x => x.Id == id);

                if (validStudent != null)
                {
                    validStudent.Date = student.Date;
                    validStudent.CreateTime = student.CreateTime;
                    validStudent.Address = student.Address;
                    return validStudent;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public bool RemoveStudent(int id)
        {
            try
            {
                // get students data from azure blob storage
                studentList = RetrieveStudentListFromAzureStorage(blobName);
                if (studentList == null) return false;

                var item = studentList.SingleOrDefault(x => x.Id == id);
                if (item != null)
                {
                    studentList.Remove(item);
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region private methods
        private List<Student> RetrieveStudentListFromAzureStorage(string blobName)
        {
            return azureService.GetBlobData<List<Student>>(blobName);
        }

        private int GenId()
        {
            var result = studentList.OrderByDescending(student => student.Id).FirstOrDefault();
            return result == null ? 1 : result.Id + 1;
        }
        #endregion
    }
}
