using Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Login
{
    public interface ILoginLogic
    {
        string CreateToken(Models.Entities.Users user);
        Models.Entities.Users Login(UserLoginDTO obj);
    }
}
