using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ShopControllerAPI.Models;

namespace ShopControllerAPI.Services
{
    public class TokenService
    {
        public static object GenerateToken(Employees employee)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("employeeId", employee.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(2), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenHash = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenHash
            };
        }
    }
}
