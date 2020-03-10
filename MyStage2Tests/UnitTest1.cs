using Microsoft.AspNetCore.Mvc;
using Moq;
using MyStage2.Controllers;
using MyStage2.Data;
using MyStage2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStage2.ViewModels;
using Xunit;

namespace MyStage2Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfUsers()
        {
            // Arrange
            //var mock = new Mock<Context>();
            //mock.Setup(repo => repo.Users).Returns(GetTestUsers());
            //var controller = new AnnounsmentsController(mock.Object);

            //// Act
            //var result = controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<Announsment>>(viewResult.Model);
            //Assert.Equal(GetTestAnnounsments().Count, model.Count());
        }


        private List<SelectListItem> GetTestUsers()
        {
            var users = new List<User>
            {
                new User { Id=1, FirstName="Tom",   LastName="qwerty"},
                new User { Id=2, FirstName="Alice", LastName="qwerty"},
                new User { Id=3, FirstName="Sam",   LastName="qwerty"},
                new User { Id=4, FirstName="Kate",  LastName="qwerty"}
            };
            return new List<SelectListItem>();
        }


        private List<Announsment> GetTestAnnounsments()
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


            return announsments;
        }

    }
}
