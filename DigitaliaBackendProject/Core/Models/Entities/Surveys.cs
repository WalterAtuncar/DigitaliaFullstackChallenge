using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Surveys
    {
        [Key]
        public int SurveyID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Question { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
