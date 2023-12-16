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

        public UsersLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return _unitOfWork.IUsers.Insert(obj);
        }

        public bool Update(Models.Entities.Users obj)
        {
            return _unitOfWork.IUsers.Update(obj);
        }
    }
}
