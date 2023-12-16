using Dapper.Contrib.Extensions;

namespace Models.Entities
{
    public class SurveyOptions
    {
        [Key]
        public int OptionID { get; set; }
        public string OptionText { get; set; }
    }
}
