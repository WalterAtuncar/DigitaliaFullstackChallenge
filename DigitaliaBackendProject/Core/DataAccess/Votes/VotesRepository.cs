using Repositories.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Votes
{
    public class VotesRepository : Repository<Models.Entities.Votes>, IVotesRepository
    {
        public VotesRepository(string _connectionString) : base(_connectionString)
        {
        }
    }
}
