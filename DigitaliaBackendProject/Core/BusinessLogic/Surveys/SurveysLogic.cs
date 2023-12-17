using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinessLogic.Surveys
{
    public class SurveysLogic : ISurveysLogic
    {
        private IUnitOfWork _unitOfWork;

        public SurveysLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Models.Entities.Surveys GetById(int id)
        {
            return _unitOfWork.ISurveys.GetById(id);
        }

        public IEnumerable<Models.Entities.Surveys> GetList()
        {
            return _unitOfWork.ISurveys.GetList();
        }

        public int Insert(Models.Entities.Surveys obj)
        {
            obj.CreationDate = DateTime.Now;
            return _unitOfWork.ISurveys.Insert(obj);
        }

        public bool Update(Models.Entities.Surveys obj)
        {
            return _unitOfWork.ISurveys.Update(obj);
        }
    }
}
