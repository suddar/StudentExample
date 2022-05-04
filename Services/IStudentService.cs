using StudentExample.Entities;

namespace StudentExample.Services
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        bool AddStudent(Student student);
        Student GetStudent(int id);
        Student UpdateStudent(int id, Student student);
        bool RemoveStudent(int id);

    }
}
