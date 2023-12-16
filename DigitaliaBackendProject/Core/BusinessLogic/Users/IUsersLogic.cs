using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Users
{
    public interface IUsersLogic
    {
        bool Update(Models.Entities.Users obj);
        int Insert(Models.Entities.Users obj);
        IEnumerable<Models.Entities.Users> GetList();
        Models.Entities.Users GetById(int id);
    }
}
