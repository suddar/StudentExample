using Microsoft.Extensions.Configuration;
using Moq;
using StudentExample.Entities;
using StudentExample.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace StudentExample.UnitTest.Services
{
    public class StudentServicesTest
    {
        #region Test GetStudents
        [Fact]
        public void GetStudents__GivenStudents_WhenGetAll_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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
        public void GetStudents__GivenNull_WhenGetAll_ThenReturnNull()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();

            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
           .Returns((List<Student>)null);

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
            Assert.Null(expect);
        }
        #endregion

        #region GetStudent
        [Fact]
        public void GetStudent__GivenStudents_WhenGetDetail_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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
            var expect = studentService.GetStudent(1);
            Assert.Equal(expect.Id, 1);
        }

        [Fact]
        public void GetStudent__GivenStudents_WhenGetDetail_ThenReturnNull()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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
            var expect = studentService.GetStudent(3);
            Assert.Equal(expect, null);
        }

        #endregion

        #region Test AddStudent
        [Fact]
        public void AddStudent__GivenNewStudent_WhenAdd_ThenReturnAddedStudent()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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
            var newStudent = new Student();//a2
            newStudent.Name = "test";
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
            var expect = studentService.AddStudent(newStudent);

            Assert.Equal(expect.Id, 1);
            Assert.Equal(expect.Name, newStudent.Name);
        }

        [Fact] // not done yet
        public void AddStudent__GivenNull_WhenAdd_Then_ReturnNull()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
            var expect = studentService.AddStudent(null);

            Assert.Null(expect);
        }
        #endregion

        #region Test UpdateStudent
        [Fact]
        public void UpdateStudent__GivenStudents_WhenUpdate_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            var students = new List<Student> {
                new Student
                {
                    Id = 0,
                    Name = "Van"
                }
            };

            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
                .Returns(students);

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);

            var newStudent = new Student { Id = 0, Name = "Hai Van" };
            var expect = studentService.UpdateStudent(newStudent.Id, newStudent);

            Assert.NotNull(expect);
            Assert.Equal(expect.Id, 0);
            Assert.Equal(expect.Name, "Hai Van");
        }

        [Fact]
        public void UpdateStudent__GivenNull_WhenUpdate_ThenReturnNull()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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
            var expect = studentService.UpdateStudent(It.IsAny<int>(), null);

            Assert.Null(expect);
        }

        #endregion

        #region Test RemoveStudent
        [Fact]
        public void RemoveStudent__GivenStudents_WhenRemove_ThenReturnOK()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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

        [Fact]
        public void RemoveStudent__GivenStudents_WhenRemove_ThenReturnFalse()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
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
            var expect = studentService.RemoveStudent(3);
            Assert.Equal(expect, false);
        }

        [Fact]
        public void RemoveStudent__GivenNull_WhenRemove_ThenReturnFalse()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>()))
                .Returns((List<Student>)null);

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
            var expect = studentService.RemoveStudent(3);
            Assert.Equal(expect, false);
        }
        #endregion
    }
}
