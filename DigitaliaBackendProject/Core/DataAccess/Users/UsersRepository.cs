using Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Users
{
    public class UsersRepository : Repository<Models.Entities.Users>, IUsersRepository
    {
        public UsersRepository(string _connectionString) : base(_connectionString)
        {
        }
    }
}
