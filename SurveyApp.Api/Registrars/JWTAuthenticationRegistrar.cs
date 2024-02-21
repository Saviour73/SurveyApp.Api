
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace SurveyApp.Api.Registrars
{
    public class JWTAuthenticationRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {

            //var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            //var jwtKey = builder.Configuration.GetSection("Jwt:SecretKey").Get<string>();
            //var jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>();


            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // .AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         ValidIssuer = jwtIssuer,
            //         ValidAudience = jwtAudience,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(jwtKey))
            //     };
            // });

          //  var secreteKey = builder.Configuration.GetValue<string>("JWT:SecretKey");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
       .AddJwtBearer(options =>
       {
           options.SaveToken = true;
           options.RequireHttpsMetadata = false;
           options.TokenValidationParameters = new TokenValidationParameters()
           {

               //ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(builder.Configuration.GetValue<string>("JWT:SecretKey"))),
               ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),
               ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),
               ValidateIssuer = true,
               ValidateAudience = true
           };
           
       });

        }
    }
}
