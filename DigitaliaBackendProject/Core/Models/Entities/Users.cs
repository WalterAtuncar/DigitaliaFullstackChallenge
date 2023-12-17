using Dapper.Contrib.Extensions;

namespace Models.Entities
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? AuthProvider { get; set; }
        public string? ProviderID { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastAccess { get; set; }
    }
}
