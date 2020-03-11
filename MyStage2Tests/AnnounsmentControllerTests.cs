using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyStage2.Controllers;
using MyStage2.Interfaces;
using MyStage2.Models;
using MyStage2.ViewModels;
using Xunit;

namespace MyStage2Tests
{
    public class AnnounsmentControllerTests
    {
        [Fact]
        public async Task IndexReturnsAViewResultWithAListOfUsers()
        {
            // Arrange
            var mockAnnounsment = new Mock<IAnnounsmentRepository>();
            var mockUser = new Mock<IUserRepository>();


            mockAnnounsment.Setup(repo => repo.Announsments).Returns(GetTestAnnounsments());
            mockUser.Setup(repo => repo.Users).Returns(GetTestUsers);

            var controller = new AnnounsmentsController(mockAnnounsment.Object, mockUser.Object);

            // Act

            var result = await controller.Index();

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<AnnounsmentsVm>(viewResult.Model);

            Assert.Equal(GetTestUsers().Count().Result, model.Users.Count());
        }


        [Fact]
        public async Task AddAnnounsmentTest()
        {
            // Arrange

            var mockAnnounsment = new Mock<IAnnounsmentRepository>();
            var mockUser = new Mock<IUserRepository>();


            mockAnnounsment.Setup(repo => repo.Announsments).Returns(GetTestAnnounsments());
            mockUser.Setup(repo => repo.Users).Returns(GetTestUsers);
            var controller = new AnnounsmentsController(mockAnnounsment.Object, mockUser.Object);


            var user = mockUser.Object.Users.First().Result;

            var newAnnounsment = new Announsment
            {
                CreateDate = DateTime.Now,
                Number = 1,
                Rating = 1,
                TextAnnounsment = "new",
                User = user
            };

            var newAnnounsmentVm = new AnnounsmentsVm
            {
                Announsment = newAnnounsment,
                SelectedUserId = user.Id
            };

            // Act
            await controller.AddAnnounsment(newAnnounsmentVm);

            mockAnnounsment.Verify(r => r.CreateAnnounsment(newAnnounsment));
        }

        [Fact]
        public async Task UpdateAnnounsmentTest()
        {
           // Arrange

           var mockAnnounsment = new Mock<IAnnounsmentRepository>();
            var mockUser = new Mock<IUserRepository>();


            mockAnnounsment.Setup(repo => repo.Announsments).Returns(GetTestAnnounsments());
            mockUser.Setup(repo => repo.Users).Returns(GetTestUsers);

            var controller = new AnnounsmentsController(mockAnnounsment.Object, mockUser.Object);


            var announsment = mockAnnounsment.Object.Announsments.First().Result;



           // Act
            announsment.TextAnnounsment = "Changed announsment text";

            var result = await controller.UpdateAnnounsment(new AnnounsmentsVm
            {
                Announsment = announsment
            });


            //Assert

            Assert.IsNotType<BadRequestResult>(result);
            Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public async Task RemoveAnnounsmentTest()
        {
            // Arrange

            var mockAnnounsment = new Mock<IAnnounsmentRepository>();
            var mockUser = new Mock<IUserRepository>();


            mockAnnounsment.Setup(repo => repo.Announsments).Returns(GetTestAnnounsments());
            mockUser.Setup(repo => repo.Users).Returns(GetTestUsers);
            var controller = new AnnounsmentsController(mockAnnounsment.Object, mockUser.Object);


            var announsment = mockAnnounsment.Object.Announsments.First().Result;

            // Act

            var result = await controller.Delete(new[] { announsment.Id });

            //Assert

            Assert.IsNotType<BadRequestResult>(result);
            Assert.IsType<RedirectToActionResult>(result);
        }




        private IAsyncEnumerable<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User {Id = 1, FirstName = "Tom", LastName = "qwerty"},
                new User {Id = 2, FirstName = "Alice", LastName = "qwerty"},
                new User {Id = 3, FirstName = "Sam", LastName = "qwerty"},
                new User {Id = 4, FirstName = "Kate", LastName = "qwerty"}
            };

            return users.ToAsyncEnumerable();
        }



        private IAsyncEnumerable<Announsment> GetTestAnnounsments()
        {
            var announsments = new List<Announsment>();

            for (var i = 0; i < 10; i++)
                announsments.Add(new Announsment
                {
                    Id = i,
                    CreateDate = DateTime.Now,
                    Number = i,
                    Rating = i,
                    TextAnnounsment = "text " + i,
                    User = new User
                    {
                        FirstName = "firstname " + i,
                        LastName = "lastName " + i
                    }
                });


            return announsments.ToAsyncEnumerable();
        }


    }
}
