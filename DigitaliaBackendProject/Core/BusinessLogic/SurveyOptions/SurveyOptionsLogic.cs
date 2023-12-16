using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinessLogic.SurveyOptions
{
    public class SurveyOptionsLogic : ISurveyoptionsLogic
    {
        private IUnitOfWork _unitOfWork;

        public SurveyOptionsLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Models.Entities.SurveyOptions GetById(int id)
        {
            return _unitOfWork.ISurveyoptions.GetById(id);
        }

        public IEnumerable<Models.Entities.SurveyOptions> GetList()
        {
            return _unitOfWork.ISurveyoptions.GetList();
        }

        public int Insert(Models.Entities.SurveyOptions obj)
        {
            return _unitOfWork.ISurveyoptions.Insert(obj);
        }

        public bool Update(Models.Entities.SurveyOptions obj)
        {
            return _unitOfWork.ISurveyoptions.Update(obj);
        }
    }
}
