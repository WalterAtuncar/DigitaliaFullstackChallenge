using Repositories.Login;
using Repositories.SurveyOptions;
using Repositories.Surveys;
using Repositories.Users;
using Repositories.Votes;

namespace UnitOfWork
{
    public interface IUnitOfWork
    {
        IUsersRepository IUsers { get; }
        ISurveyoptionsRepository ISurveyoptions { get; }
        ISurveysRepository ISurveys { get; }
        IVotesRepository IVotes { get; }
        ILoginRepository ILogin { get; }
    }
}
