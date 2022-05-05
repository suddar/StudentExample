using Newtonsoft.Json;
using StudentExample.Entities;

namespace StudentExample.Services
{
    public class StudentService : IStudentService, IDisposable
    {
        private IAzureService azureService;
        private List<Student> studentsList;
        private readonly IConfiguration configuration;

        private string blobName;

        public StudentService(IAzureService azureService, IConfiguration configuration)
        {
            this.azureService = azureService;
            this.configuration = configuration;
            blobName = configuration["BlobName"];

            // get students data from azure blob storage
            studentsList = RetrieveStudentsFromAzureStorage("student-test");
        }

        public List<Student> GetStudents()
        {
            if (studentsList == null) return null;
            return studentsList;
        }

        public Student? GetStudent(int id)
        {
            if (studentsList == null) return null;
            return studentsList.FirstOrDefault(student => student.Id == id);
        }

        public Student AddStudent(Student student)
        {
            try
            {
                student.CreateTime = DateTime.Now;
                student.Id = GenId();
                studentsList.Add(student);

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
                    validStudent.Name = student.Name;
                    validStudent.Date = student.Date;
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
                var item = studentsList.FirstOrDefault(x => x.Id == id);
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
            var ret = azureService.GetBlobData<List<Student>>(blobName);
            return ret;
        }

        private int GenId()
        {
            var result = studentsList.OrderByDescending(student => student.Id).FirstOrDefault();
            return result == null ? 1 : result.Id + 1;
        }

        public void Dispose()
        {
            azureService.UploadData(JsonConvert.SerializeObject(studentsList), blobName);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
