using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace JWT.Encrypt
{
    public class EncryptServices : IEncryptServices
    {
        private IConfiguration _configuration;
        public EncryptServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Encrypt(string pData)
        {
            var salt = _configuration.GetValue<string>("jwtPassword");
            var saltedPData = String.Concat(pData, salt);

            UnicodeEncoding parser = new UnicodeEncoding();
            byte[] _original = parser.GetBytes(saltedPData);

            MD5CryptoServiceProvider Hash = new MD5CryptoServiceProvider();
            byte[] _encrypt = Hash.ComputeHash(_original);

            return Convert.ToBase64String(_encrypt);
        }
    }
}
