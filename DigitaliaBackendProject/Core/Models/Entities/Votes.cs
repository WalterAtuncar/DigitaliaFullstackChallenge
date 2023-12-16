using Dapper.Contrib.Extensions;

namespace Models.Entities
{
    public class Votes
    {
        [Key]
        public int VoteID { get; set; }
        public int SurveyID { get; set; }
        public int OptionID { get; set; }
        public int UserID { get; set; }
        public DateTime? VoteDate { get; set; }
    }
}
