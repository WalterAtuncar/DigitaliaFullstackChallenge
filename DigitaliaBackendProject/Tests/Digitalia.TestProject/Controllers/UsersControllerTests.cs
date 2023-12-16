using BusinessLogic.Users;
using Digitalia.Fullstack.Challenge.Controllers.Users;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalia.TestProject.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersLogic> mockLogic;
        private readonly usersController controller;
        private readonly List<Users> fakeUsersList;
        private readonly Users newUser;
        private readonly Users existingUser;

        public UsersControllerTests()
        {
            mockLogic = new Mock<IUsersLogic>();
            controller = new usersController(mockLogic.Object);

            fakeUsersList = new List<Users>
            {
                new Users { UserID = 1, UserName = "WalterA", Email = "walter@example.com" },
                new Users { UserID = 2, UserName = "RGBA", Email = "rgb@example.com" }
            };

            newUser = new Users { UserName = "NewUser", Email = "newuser@example.com", PasswordHash = "HashedPassword" };
            existingUser = new Users { UserID = 3, UserName = "ExistingUser", Email = "existinguser@example.com", PasswordHash = "HashedPassword" };
        }

        [Fact(DisplayName = "GetList returns list of users")]
        public void GetList_ReturnsListOfUsers()
        {
            mockLogic.Setup(logic => logic.GetList()).Returns(fakeUsersList);

            var result = controller.GetList();

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var usersList = Assert.IsType<List<Users>>(responseDTO.objModel);
            Assert.NotNull(usersList);
            Assert.Equal(2, usersList.Count);
        }

        [Fact(DisplayName = "GetById returns a user")]
        public void GetById_ReturnsAUser()
        {
            int testId = 1;
            mockLogic.Setup(logic => logic.GetById(testId)).Returns(fakeUsersList[0]);

            var result = controller.GetById(testId);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var user = Assert.IsType<Users>(responseDTO.objModel);
            Assert.NotNull(user);
            Assert.Equal(testId, user.UserID);
        }

        [Fact(DisplayName = "Insert adds a new user and returns its ID")]
        public void Insert_AddsNewUserAndReturnsId()
        {
            mockLogic.Setup(logic => logic.Insert(newUser)).Returns(2);

            var result = controller.Insert(newUser);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(2, responseDTO.objModel); 
        }

        [Fact(DisplayName = "Update modifies an existing user and returns success status")]
        public void Update_ModifiesExistingUserAndReturnsSuccessStatus()
        {
            mockLogic.Setup(logic => logic.Update(existingUser)).Returns(true);

            var result = controller.Update(existingUser);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.True((bool)responseDTO.objModel);
        }


    }
}
