using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Token.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public Object GetToken()
        {
            string key = WebConfigurationManager.AppSettings["TokenSecretKey"];
            var issuer = WebConfigurationManager.AppSettings["TokenIssuerURL"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "saiLife"));

            var token = new JwtSecurityToken(issuer,
                            issuer,
                            permClaims,
                            expires: DateTime.Now.AddDays(Convert.ToInt32(WebConfigurationManager.AppSettings["TokenExpire"])),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }
    }
}
