using Quartz;
using StudentExample.Services;

namespace StudentExample.Jobs
{
    public class StudentIncremental : IJob
    {
        private readonly IStudentService studentService;

        public StudentIncremental(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("start");
            studentService.GetStudents().ForEach(student => { student.Count++; });
            Console.WriteLine("end");
        }
    }
}
