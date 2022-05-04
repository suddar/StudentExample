using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using StudentExample.Entities;
using System.Text;

namespace StudentExample.Services
{
    public class StudentService : IStudentService
    {
        //private IAzureService azureService;
        private List<Student> studentlist = new();

        public StudentService()
        {
            //this.azureService = azureService;

        }

        public bool AddStudent(Student student)
        {
            try
            {

                student.Id = GenId();
                studentlist.Add(student);
                //azureService.UploadData();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GenId()
        {
            var result = studentlist.OrderByDescending(student => student.Id).FirstOrDefault();
            return result == null ? 1 : result.Id + 1;
        }

        public Student? GetStudent(int id)
        {
            var student = studentlist.FirstOrDefault(student => student.Id == id);
            return student;
        }

        public List<Student> GetStudents()
        {
            return studentlist;
        }

        public Student? UpdateStudent(int id, Student student)
        {
            try
            {
                // find student by id
                var validStudent = studentlist.FirstOrDefault(x => x.Id == id);

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
                var item = studentlist.SingleOrDefault(x => x.Id == id);
                if (item != null)
                {
                    studentlist.Remove(item);
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
