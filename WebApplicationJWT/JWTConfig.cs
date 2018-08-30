using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationJWT
{
    public class JWTConfig
    {
        public static SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("11111231231231231231231232131231231231231"));

        public static SigningCredentials SigningCredentials = new SigningCredentials(JWTConfig.SigningKey, SecurityAlgorithms.HmacSha256);

        public const string Issure = "Issure";

        public const string Audience = "Audience";

    }
}
