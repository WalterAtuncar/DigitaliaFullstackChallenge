using JWT.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinessLogic.Users
{
    public class UsersLogic : IUsersLogic
    {
        private IUnitOfWork _unitOfWork;
        private IEncryptServices _encryptServices;

        public UsersLogic(IUnitOfWork unitOfWork, IEncryptServices encryptServices)
        {
            _unitOfWork = unitOfWork;
            _encryptServices = encryptServices;
        }

        public Models.Entities.Users GetById(int id)
        {
            return _unitOfWork.IUsers.GetById(id);
        }

        public IEnumerable<Models.Entities.Users> GetList()
        {
            return _unitOfWork.IUsers.GetList();
        }

        public int Insert(Models.Entities.Users obj)
        {
            if (!String.IsNullOrEmpty(obj.PasswordHash))
            {
                obj.PasswordHash = _encryptServices.Encrypt(obj.PasswordHash);
            }
            obj.CreationDate = DateTime.Now;
            return _unitOfWork.IUsers.Insert(obj);
        }

        public bool Update(Models.Entities.Users obj)
        {
            return _unitOfWork.IUsers.Update(obj);
        }
    }
}
