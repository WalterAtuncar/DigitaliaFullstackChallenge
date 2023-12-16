using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.SurveyOptions
{
    public interface ISurveyoptionsLogic
    {
        bool Update(Models.Entities.SurveyOptions obj);
        int Insert(Models.Entities.SurveyOptions obj);
        IEnumerable<Models.Entities.SurveyOptions> GetList();
        Models.Entities.SurveyOptions GetById(int id);
    }
}
