using StudentExample.Entities;

namespace StudentExample.Services
{
    public class StudentService : IStudentService
    {
        public bool AddStudent(Student student)
        {
            return new();
        }

        public Student getStudent(int id)
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

        public bool UpdateStudent(int id, Student student)
        {
            return true;
        }
    }
}
