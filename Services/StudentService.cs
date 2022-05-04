using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using StudentExample.Entities;
using System.Text;

namespace StudentExample.Services
{
    public class StudentService : IStudentService
    {
        private List<Student> studentlist = new List<Student>();

        public StudentService()
        {

            Student student = new Student();
            student.Name = "Mai Thi Hoa";
            student.Address = "Ha noi";
            studentlist.Add(student);

            student = new Student();
            student.Name = "Nguyen Van Nam";
            student.Address = "Ho chi minh";
            studentlist.Add(student);
        }

        public bool AddStudent(Student student)
        {
            try
            {
                studentlist.Add(student);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Student GetStudent(int id)
        {
            return studentlist[id];
        }

        public List<Student> GetStudents()
        {
            return studentlist;
        }

        public Student UpdateStudent(int id, Student student)
        {
            return studentlist[id] = student;
        }

        public bool RemoveStudent(int id)
        {
            try
            {
                studentlist.RemoveAt(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
