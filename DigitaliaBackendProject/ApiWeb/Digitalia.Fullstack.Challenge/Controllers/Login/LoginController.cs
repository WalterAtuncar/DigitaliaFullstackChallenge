using BusinessLogic.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Request;

namespace Digitalia.Fullstack.Challenge.Controllers.Login
{
    [Route("api/authentication")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginLogic _login;
        public ResponseDTO _ResponseDTO;
        public LoginController(ILoginLogic login)
        {
            _login = login;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginDTO obj)
        {
            _ResponseDTO = new ResponseDTO();
            try
            {
                var user = _login.Login(obj);
                if (user != null)
                {
                    _ResponseDTO.token = _login.CreateToken(user);
                }
                return Ok(_ResponseDTO.Success(_ResponseDTO, user));
            }
            catch (Exception e)
            {
                return BadRequest(_ResponseDTO.Failed(_ResponseDTO, e.Message));
            }
        }
    }
}
