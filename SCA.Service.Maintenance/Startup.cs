using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SCA.Service.Maintenance.Data;
using SCA.Service.Maintenance.Services;
using SCA.Shared.Extensions;

namespace SCA.Maintenance
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
            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddContexto<MaintenanceContext>(Configuration.GetConnectionString("MaintenanceContext"), "SCA.Service.Maintenance");

            services.AddAutenticacao();

            services.AddScoped<SeedingService>();
            services.AddScoped<ManutencaoService>();
            services.AddScoped<InsumoService>();
            services.AddScoped<MarcaService>();
            services.AddScoped<TipoService>();

            services.AddSwaggerDocumentation("SCA.Service.Maintenance");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }

            app.UseSwagger();

            app.UseSwaggerDocumentation("SCA.Service.Maintenance v1.0");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
