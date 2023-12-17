using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Votes
{
    public interface IVotesRepository : IRepository<Models.Entities.Votes>
    {
        IEnumerable<result> getResults(int id);
        int validateVote(validateVote obj);
    }
}
