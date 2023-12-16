using Dapper.Contrib.Extensions;

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
