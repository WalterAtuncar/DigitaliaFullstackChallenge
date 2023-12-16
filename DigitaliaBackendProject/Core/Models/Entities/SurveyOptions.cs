using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class SurveyOptions
    {
        [Key]
        public int OptionID { get; set; }
        public string OptionText { get; set; }
    }
}
