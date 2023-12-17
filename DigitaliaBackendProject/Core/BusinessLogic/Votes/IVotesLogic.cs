using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Votes
{
    public interface IVotesLogic
    {
        bool Update(Models.Entities.Votes obj);
        int Insert(Models.Entities.Votes obj);
        IEnumerable<Models.Entities.Votes> GetList();
        Models.Entities.Votes GetById(int id);
        int validateVote(validateVote obj);
        IEnumerable<result> getResults(int id);
    }
}
