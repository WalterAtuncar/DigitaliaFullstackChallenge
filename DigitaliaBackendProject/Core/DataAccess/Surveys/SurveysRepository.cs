using Repositories.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Surveys
{
    public class SurveysRepository : Repository<Models.Entities.Surveys>, ISurveysRepository
    {
        public SurveysRepository(string _connectionString) : base(_connectionString)
        {
        }
    }
}
