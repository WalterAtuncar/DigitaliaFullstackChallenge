using Repositories.SurveyOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SurveyOptions
{
    public class SurveyOptionsRepository : Repository<Models.Entities.SurveyOptions>, ISurveyoptionsRepository
    {
        public SurveyOptionsRepository(string _connectionString) : base(_connectionString)
        {
        }
    }
}
