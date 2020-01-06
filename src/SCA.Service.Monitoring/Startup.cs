using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SCA.Service.Monitoring.Data;
using SCA.Service.Monitoring.Services;
using SCA.Shared.Extensions;

namespace SCA.Monitoring
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

            services.AddContexto<MonitoramentoContext>(Configuration.GetConnectionString("MonitoringContext"), "SCA.Service.Monitoring");

            services.AddAutenticacao();

            services.AddScoped<SeedingService>();
            services.AddScoped<RegiaoService>();
            services.AddScoped<BarragemService>();
            services.AddScoped<SensorService>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SCA.Service.Monitoring", Version = "v1" });
            });
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

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCA.Service.Monitoring V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
