using Dapper;
using Models.Request;
using Repositories.Votes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Response;

namespace DataAccess.Votes
{
    public class VotesRepository : Repository<Models.Entities.Votes>, IVotesRepository
    {
        public VotesRepository(string _connectionString) : base(_connectionString)
        {
        }

        public IEnumerable<result> getResults(int SurveyID)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SurveyID", SurveyID);

                    return connection.Query<result>("[dbo].[spGetSurveyResults]", parameters, commandType: CommandType.StoredProcedure);
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

        public int validateVote(validateVote obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@surveyID", obj.surveyID);
                    parameters.Add("@userID", obj.userID);

                    return connection.Query<int>("[dbo].[spValidateVote]", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
