using Dapper;
using Models.Request;
using Repositories.Login;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Login
{
    public class LoginRepository : Repository<UserLoginDTO>, ILoginRepository
    {
        public LoginRepository(string _connectionString) : base(_connectionString)
        {
        }

        public Models.Entities.Users Login(UserLoginDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserName", obj.UserName);
                    parameters.Add("@Password", obj.Password);
                    parameters.Add("@ProviderID", obj.ProviderID);

                    return connection.Query<Models.Entities.Users>("[dbo].[spValidateLogin]", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
