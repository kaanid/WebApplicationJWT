using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApplicationJWT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=JWTConfig.SigningKey,
                
                ValidateIssuer=true,
                ValidIssuer= JWTConfig.Issure,

                ValidateAudience=true,
                ValidAudience= JWTConfig.Audience,

                RequireExpirationTime = false,
                ValidateLifetime =true,
                ClockSkew=TimeSpan.Zero
            };

            services.AddAuthentication(options => {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=> {
                options.Audience = JWTConfig.Audience;
                options.ClaimsIssuer = JWTConfig.Issure;
                options.TokenValidationParameters = tokenValidationParameters;
                options.SaveToken = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TokenAuthenticationMiddleware>();

            app.UseMvc();

            app.UseAuthentication();
        }
    }
}
