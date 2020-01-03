using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SCA.Shared.Domain.Properties;
using System;
using System.Text;

namespace SCA.Shared.Startup
{
    public static class ScaStartup
    {
        public static void AddDbContext<T>(IServiceCollection services, string connectionString, string migrationsAssembly) where T: DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseMySql(connectionString, builder => builder.MigrationsAssembly(migrationsAssembly)));
        }

        public static void AddAuthentication(IServiceCollection services)
        {
            //Provide a secret key to Encrypt and Decrypt the Token
            var SecretKey = Encoding.ASCII.GetBytes(Token.Key);
            //Configure JWT Token Authentication
            services.AddAuthentication(auth => {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(token => {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    //Same Secret key will be used while creating the token
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    //Usually, this is your application base URL
                    ValidIssuer = Token.Issuer,
                    ValidateAudience = true,
                    //Here, we are creating and using JWT within the same application.
                    //In this case, base URL is fine.
                    //If the JWT is created using a web service, then this would be the consumer URL.
                    ValidAudience = Token.Audience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
