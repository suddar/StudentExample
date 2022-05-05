using Microsoft.Extensions.Configuration;
using Moq;
using StudentExample.Entities;
using StudentExample.Services;
using System.Collections.Generic;
using Xunit;

namespace StudentExample.UnitTest.Services
{
    public class StudentServicesTest
    {
        [Fact]
        public void GivenStudents_WhenGetAll_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
           .Returns(new List<Student>() { new Student { Id = 1 }, new Student { Id = 2 } });

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
            var expect = studentService.GetStudents();

            // Assert
            Assert.Equal(expect.Count, 2);
            Assert.Equal(expect[0].Id, 1);
        }

        [Fact]
        public void GivenNewStudent_WhenAdd_ThenReturnAddedStudent()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
              .Returns(new List<Student>());

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var newStudent = new Student();
            newStudent.Name = "test";
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
            var expect = studentService.AddStudent(newStudent);
            Assert.Equal(expect.Id, 1);
            Assert.Equal(expect.Name, newStudent.Name);
        }

        [Fact]
        public void GivenStudents_WhenGetDetail_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>())).Returns(It.IsAny<List<Student>>());
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
         .Returns(new List<Student>() { new Student { Id = 1 }, new Student { Id = 2 } });

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
         var expect =   studentService.GetStudent(1);
            Assert.Equal(expect.Id, 1);
        }

        [Fact]
        public void GivenStudents_WhenUpdate_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
        .Returns(new List<Student>() { new Student { Id = 1 }, new Student { Id = 2 } });

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, configuration);
            var expect =  studentService.UpdateStudent(1, new Student { Id=1,Name="test"});
            Assert.Equal(expect.Name, "test");
        }

        [Fact]
        public void GivenStudents_WhenRemove_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
       .Returns(new List<Student>() { new Student { Id = 1 }, new Student { Id = 2 } });

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, configuration);
            studentService.AddStudent(It.IsAny<Student>());
            var expect = studentService.RemoveStudent(2);
            Assert.Equal(expect, true);
        }
    }
}
