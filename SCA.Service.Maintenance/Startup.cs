using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SCA.Service.Maintenance.Data;
using SCA.Service.Maintenance.Services;
using SCA.Shared.Extensions;
using System.Collections.Generic;

namespace SCA.Maintenance
{
    public class Startup
    {
        private readonly string routePrefix;
        private readonly string prefixUrl;
        private readonly string basePath;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            this.routePrefix = configuration.GetSection("ConfigApp").GetSection("routePrefix").Value;
            this.prefixUrl = configuration.GetSection("ConfigApp").GetSection("prefixUrl").Value;
            this.basePath = "";

            if (!string.IsNullOrEmpty(this.prefixUrl))
            {
                string host = configuration.GetSection("ConfigApp").GetSection("swaggerHost").Value;
                string port = configuration.GetSection("ConfigApp").GetSection("swaggerHostPort").Value;
                this.basePath = $"http://{host}:{port}{this.prefixUrl}";
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddContexto<MaintenanceContext>(Configuration.GetConnectionString("MaintenanceContext"), "SCA.Service.Maintenance");

            services.AddAutenticacao();

            services.AddScoped<SeedingService>();
            services.AddScoped<ManutencaoService>();

            services.AddSwaggerDocumentation("SCA.Service.Maintenance");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            seedingService.Seed();

            app.UseSwagger(c => {
                c.PreSerializeFilters.Add((swagger, httpReq) => {
                    // swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = string.IsNullOrEmpty(this.basePath) ? $"{httpReq.Scheme}://{httpReq.Host.Value}" : this.basePath } };
                });
            });

            app.UseSwaggerDocumentation("SCA.Service.Maintenance v1.0", this.routePrefix, this.prefixUrl);

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
