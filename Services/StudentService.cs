using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using StudentExample.Entities;
using System.Text;

namespace StudentExample.Services
{
    public class StudentService : IStudentService, IDisposable
    {
        private IAzureService azureService;
        private List<Student> studentsList;
        private string blobName;

        public StudentService(IAzureService azureService)
        {
            this.azureService = azureService;
            blobName = "student-test";

            // get students data from azure blob storage
            studentsList = RetrieveStudentsFromAzureStorage("student-test");
        }

        public List<Student> GetStudents()
        {
            return studentsList;
        }

        public Student? GetStudent(int id)
        {
            return studentsList.FirstOrDefault(student => student.Id == id);
        }

        public Student AddStudent(Student student)
        {
            try
            {
                student.Id = GenId();
                studentsList.Add(student);

                //up load data to a blob
                var resultCode = azureService.UploadData(JsonConvert.SerializeObject(studentsList), blobName);
                if (resultCode == 201) return null;

                return student;
            }
            catch (Exception)
            {
                return null;
            }
            return student;
        }

        public Student? UpdateStudent(int id, Student student)
        {
            try
            {
                // find student by id
                var validStudent = studentsList.FirstOrDefault(x => x.Id == id);

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
                var item = studentsList.SingleOrDefault(x => x.Id == id);
                if (item != null)
                {
                    studentsList.Remove(item);
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
        private List<Student> RetrieveStudentsFromAzureStorage(string blobName)
        {
            return azureService.GetBlobData<List<Student>>(blobName);
        }

        private int GenId()
        {
            var result = studentsList.OrderByDescending(student => student.Id).FirstOrDefault();
            return result == null ? 1 : result.Id + 1;
        }

        public void Dispose()
        {
            azureService.UploadData(JsonConvert.SerializeObject(studentsList), blobName);
            // Dispose of unmanaged resources.
            //Dispose();

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
