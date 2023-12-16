namespace Models.Request
{
    public class UserLoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProviderID { get; set; }
    }
}
