using Moq;
using StudentExample.Controllers;
using StudentExample.Entities;
using StudentExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StudentExample.UnitTest.Controllers
{
    public class StudentControllerTest
    {
        [Fact]
        public void Get()
        {
            StudentController studentController = new(It.IsAny<IStudentService>());
            studentController.Get();
        }

        [Fact]
        public void GetStudent()
        {
            StudentController studentController = new(It.IsAny<IStudentService>());
            studentController.GetStudent(It.IsAny<int>());
        }

        [Fact]
        public void CreateStudent()
        {
            StudentController studentController = new(It.IsAny<IStudentService>());
            studentController.CreateStudent(It.IsAny<Student>());
        }

        [Fact]
        public void UpdateStudent()
        {
            StudentController studentController = new(It.IsAny<IStudentService>());
            studentController.UpdateStudent(It.IsAny<int>(), It.IsAny<Student>());
        }

        [Fact]
        public void Delete()
        {
            StudentController studentController = new(It.IsAny<IStudentService>());
            studentController.Delete(It.IsAny<int>());
        }
    }
}
