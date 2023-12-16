using JWT.JWT;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Digitalia.TestProject.TokenService
{
    public class TokenServicesTests
    {
        private readonly ITokenServices tokenService;
        private readonly IConfiguration configuration;

        public TokenServicesTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
            {"jwtPassword", "15O135M7E141G5205A16518211931"}
        };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            tokenService = new TokenServices(configuration);
        }

        [Fact]
        public void CreateToken_GeneratesValidToken()
        {
            var user = new Users { UserID = 1, UserName = "testuser" };

            var token = tokenService.CreateToken(user);
            Assert.False(string.IsNullOrWhiteSpace(token));

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            Assert.NotNull(jwtToken);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "name");
            Assert.NotNull(userIdClaim);
            Assert.Equal(user.UserID.ToString(), userIdClaim.Value);
        }
    }
}
