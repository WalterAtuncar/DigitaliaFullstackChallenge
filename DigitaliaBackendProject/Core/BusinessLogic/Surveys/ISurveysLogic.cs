using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Surveys
{
    public interface ISurveysLogic
    {
        bool Update(Models.Entities.Surveys obj);
        int Insert(Models.Entities.Surveys obj);
        IEnumerable<Models.Entities.Surveys> GetList();
        Models.Entities.Surveys GetById(int id);
    }
}
