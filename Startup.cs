using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;
using System.Web.Configuration;
using AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode;

[assembly: OwinStartup(typeof(Token.Startup))]

namespace Token
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = WebConfigurationManager.AppSettings["TokenIssuerURL"],  
                        ValidAudience = WebConfigurationManager.AppSettings["TokenIssuerURL"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfigurationManager.AppSettings["TokenSecretKey"]))
                    }
                });
        }
    }
}
