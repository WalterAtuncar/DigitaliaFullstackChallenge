using DataAccess.Login;
using DataAccess.SurveyOptions;
using DataAccess.Surveys;
using DataAccess.Users;
using DataAccess.Votes;
using Repositories.Login;
using Repositories.SurveyOptions;
using Repositories.Surveys;
using Repositories.Users;
using Repositories.Votes;
using UnitOfWork;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository IUsers { get; }

        public ISurveyoptionsRepository ISurveyoptions { get; }

        public ISurveysRepository ISurveys { get; }

        public IVotesRepository IVotes { get; }

        public ILoginRepository ILogin { get; }

        public UnitOfWork(string connectionString)
        {
            IUsers = new UsersRepository(connectionString);
            ISurveyoptions = new SurveyOptionsRepository(connectionString);
            ISurveys = new SurveysRepository(connectionString);
            IVotes = new VotesRepository(connectionString);
            ILogin = new LoginRepository(connectionString);
        }        
    }
}
