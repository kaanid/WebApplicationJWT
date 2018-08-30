using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplicationJWT.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<string>> Get()
        {
            var user = new UserModel()
            {
                Email="asdf@163.com",
                FullName="Becaasdfsfe dfef df efe"
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.Role, "Administrator"),
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme);

            //await HttpContext.Authentication.ChallengeAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            //await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


            SigningCredentials signingCredentials = new SigningCredentials(JWTConfig.SigningKey, SecurityAlgorithms.HmacSha256);

            //JwtHeader h = new JwtHeader(signingCredentials);
            //JwtPayload p = new JwtPayload("Audience", "Issure", claims,null,DateTime.Now.AddDays(1));
            //JwtSecurityToken token = new JwtSecurityToken(h,p);

            JwtSecurityToken token = new JwtSecurityToken(JWTConfig.Issure,JWTConfig.Audience,claims,null,DateTime.Now.AddDays(1), signingCredentials);
            //token.SigningCredentials
            var handler = new JwtSecurityTokenHandler();
            var strToken=handler.WriteToken(token);

            //var result = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme,);
            return new string[] { "value1", "value2", strToken };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
