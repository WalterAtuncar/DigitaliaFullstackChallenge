using DataAccess.SurveyOptions;
using DataAccess.Surveys;
using DataAccess.Users;
using DataAccess.Votes;
using Repositories.SurveyOptions;
using Repositories.Surveys;
using Repositories.Users;
using Repositories.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository IUsers { get; }

        public ISurveyoptionsRepository ISurveyoptions { get; }

        public ISurveysRepository ISurveys { get; }

        public IVotesRepository IVotes { get; }

        public UnitOfWork(string connectionString)
        {
            IUsers = new UsersRepository(connectionString);
            ISurveyoptions = new SurveyOptionsRepository(connectionString);
            ISurveys = new SurveysRepository(connectionString);
            IVotes = new VotesRepository(connectionString);
        }        
    }
}
