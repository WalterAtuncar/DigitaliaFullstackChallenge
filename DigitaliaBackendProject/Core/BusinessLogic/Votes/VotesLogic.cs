using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinessLogic.Votes
{
    public class VotesLogic : IVotesLogic
    {
        private IUnitOfWork _unitOfWork;

        public VotesLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Models.Entities.Votes GetById(int id)
        {
            return _unitOfWork.IVotes.GetById(id);
        }
        public IEnumerable<Models.Entities.Votes> GetList()
        {
            return _unitOfWork.IVotes.GetList();
        }

        public IEnumerable<result> getResults(int id)
        {
            return _unitOfWork.IVotes.getResults(id);
        }

        public int Insert(Models.Entities.Votes obj)
        {
            obj.VoteDate = DateTime.Now;
            return _unitOfWork.IVotes.Insert(obj);
        }

        public bool Update(Models.Entities.Votes obj)
        {            
            return _unitOfWork.IVotes.Update(obj);
        }

        public int validateVote(validateVote obj)
        {
            return _unitOfWork.IVotes.validateVote(obj);
        }
    }
}
