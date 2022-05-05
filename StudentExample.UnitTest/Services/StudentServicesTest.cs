using Microsoft.Extensions.Configuration;
using Moq;
using StudentExample.Entities;
using StudentExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StudentExample.UnitTest.Services
{
    public class StudentServicesTest
    {
        [Fact]
        public void GetStudents()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
            studentService.GetStudents();

            // Assert
        }

        [Fact]
        public void AddStudent()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
            studentService.AddStudent(It.IsAny<Student>());
        }

        [Fact]
        public void GetStudent()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.GetBlobData<List<Student>>(It.IsAny<string>())).Returns(It.IsAny<List<Student>>());

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, mockConfiguration);
            studentService.GetStudent(It.IsAny<int>());
        }

        [Fact]
        public void UpdateStudent()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());

            var appsettings = new Dictionary<string, string> {
                {"ConnectionString", "ConnectionStringValues"},
                {"ContainerName", "ContainerNameValue"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            // Act
            var studentService = new StudentService(mockIAzureService.Object, configuration);
            studentService.UpdateStudent(It.IsAny<int>(), It.IsAny<Student>());
        }

        [Fact]
        public void RemoveStudent()
        {
            // Arrange
            var mockIAzureService = new Mock<IAzureService>();
            mockIAzureService.Setup(repo => repo.UploadData(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<int>());

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
            studentService.RemoveStudent(It.IsAny<int>());
        }
    }
}
