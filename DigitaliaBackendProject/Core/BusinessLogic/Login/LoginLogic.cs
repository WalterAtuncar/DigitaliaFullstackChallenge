using JWT.Encrypt;
using JWT.JWT;
using Models.Request;
using UnitOfWork;

namespace BusinessLogic.Login
{
    public class LoginLogic : ILoginLogic
    {
        private IUnitOfWork _unitOfWork;
        private ITokenServices _tokenServices;
        private IEncryptServices _encryptServices;
        public LoginLogic(IUnitOfWork unitOfWork, ITokenServices tokenServices, IEncryptServices encryptServices)
        {
            _unitOfWork = unitOfWork;
            _tokenServices = tokenServices;
            _encryptServices = encryptServices;
        }

        public string CreateToken(Models.Entities.Users user)
        {
            return _tokenServices.CreateToken(user);
        }

        public Models.Entities.Users Login(UserLoginDTO obj)
        {
            obj.Password = _encryptServices.Encrypt(obj.Password);
            return _unitOfWork.ILogin.Login(obj);
        }
    }
}
