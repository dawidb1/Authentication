using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Model.Auth
{
    public class TokenHandler
    {
        private JwtSecurityToken jwtSecurityToken;
        private SymmetricSecurityKey symmetricSecurityKey;
        private Claim[] claims;

        public TokenResultModel GetToken(string username)
        {
            SetClaims(username);
            SetSymmetricSecurityKey();
            SetToken();

            return GetWritenToken();
        }

        private void SetToken()
        {
            this.jwtSecurityToken = new JwtSecurityToken(
                     issuer: "Biometria",
                     audience: "Biometria",
                     expires: DateTime.Now.AddHours(1),
                     claims: claims,
                     signingCredentials: new SigningCredentials(this.symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                 );
        }

        private TokenResultModel GetWritenToken()
        {
            var writenToken = new JwtSecurityTokenHandler().WriteToken(this.jwtSecurityToken);

            return new TokenResultModel
            {
                Token = writenToken,
                Expiration = this.jwtSecurityToken.ValidTo
            };
        }

        private void SetClaims(string username)
        {
            this.claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
        }

        private void SetSymmetricSecurityKey()
        {
            var securityKey = "mySecurityKeydsfasdfasfasfasf.in";
            this.symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
