using Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Login
{
    public interface ILoginRepository : IRepository<UserLoginDTO>
    {
        Models.Entities.Users Login(UserLoginDTO obj);
    }
}
