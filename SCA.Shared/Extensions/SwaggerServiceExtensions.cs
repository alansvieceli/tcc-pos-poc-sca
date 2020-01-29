using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;

namespace SCA.Shared.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string title)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1.0" });

            


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

            });

            return services;

        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string title, string routePrefix, string prefixUrl)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                //"/swagger/v1/swagger.json"
                c.SwaggerEndpoint($"{prefixUrl}/swagger/v1/swagger.json", title);
                c.RoutePrefix = routePrefix;
                c.DocumentTitle = "Documentação";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
