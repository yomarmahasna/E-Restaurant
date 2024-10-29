using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Restaurant.Helper
{

        public static class TokenHelper
        {
            public async static Task<string> GenerateToken(string personId, string role)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes("Dear Developer, I hope you are doing well. Your task is to develop an Application Programming Interface (API) for an E-Restaurant System using ASP.NET Core 8 and Microsoft SQL Server. This API will serve as the backend for a restaurant system where users can browse the menu, place orders, and leave feedback. Admins will manage the menu, process orders, and handle customer reviews.\r\n\r\nUser Story:\r\nWe need an API to manage an online restaurant system where users can browse the restaurant's menu, create an account, place orders, and leave reviews. Admins will manage the restaurant's menu, handle orders, and process user feedback. Users should be able to view the restaurant's menu, add items to their cart, place an order, and review the ordered items. The system must also allow the restaurant to update stock availability of menu items and manage user orders.\r\n");
                var tokenDescriptior = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("PersonId",personId),
                        new Claim(ClaimTypes.Role,role)
                    }),
                    Expires = DateTime.Now.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey)
                    , SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptior);
                return tokenHandler.WriteToken(token);
            }
            public async static Task<bool> ValidateToken(string tokenString, string role)
            {
                String toke = "Bearer " + tokenString;
                var jwtEncodedString = toke.Substring(7);
                var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                var roleString = (token.Claims.First(c => c.Type == "role").Value.ToString());
                if (token.ValidTo > DateTime.UtcNow && roleString.Equals(role, StringComparison.OrdinalIgnoreCase))
                {

                    return true;
                }
                return false;
            }
        }
    }

