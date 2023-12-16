using Models.Entities;

namespace JWT.JWT
{
    public interface ITokenServices
    {
        string CreateToken(Users user);
    }
}
