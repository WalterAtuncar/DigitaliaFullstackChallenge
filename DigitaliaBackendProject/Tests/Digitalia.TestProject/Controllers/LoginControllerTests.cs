using BusinessLogic.Login;
using Digitalia.Fullstack.Challenge.Controllers.Login;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;
using Models.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalia.TestProject.Controllers
{
    public class LoginControllerTests
    {
        private readonly Mock<ILoginLogic> mockLogic;
        private readonly LoginController controller;
        private readonly UserLoginDTO validLoginDTO;
        private readonly Users validUser;

        public LoginControllerTests()
        {
            mockLogic = new Mock<ILoginLogic>();
            controller = new LoginController(mockLogic.Object);

            validLoginDTO = new UserLoginDTO
            {
                UserName = "testuser",
                Password = "testpassword"
            };

            validUser = new Users
            {
                UserID = 1,
                UserName = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "hashedpassword"
            };
        }

        [Fact(DisplayName = "Login returns user and token for valid credentials")]
        public void Login_ReturnsUserAndTokenForValidCredentials()
        {            
            mockLogic.Setup(logic => logic.Login(validLoginDTO)).Returns(validUser);
            mockLogic.Setup(logic => logic.CreateToken(validUser)).Returns("fakeToken");

            var result = controller.Login(validLoginDTO);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var user = Assert.IsType<Users>(responseDTO.objModel);
            Assert.Equal("fakeToken", responseDTO.token);
            Assert.NotNull(user);
            Assert.Equal(validUser.UserName, user.UserName);
        }

    }
}
