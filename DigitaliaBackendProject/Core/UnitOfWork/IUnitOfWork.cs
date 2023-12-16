using Repositories.SurveyOptions;
using Repositories.Surveys;
using Repositories.Users;
using Repositories.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public interface IUnitOfWork
    {
        IUsersRepository IUsers { get; }
        ISurveyoptionsRepository ISurveyoptions { get; }
        ISurveysRepository ISurveys { get; }
        IVotesRepository IVotes { get; }
    }
}
