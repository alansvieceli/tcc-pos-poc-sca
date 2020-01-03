using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SCA.Service.Inputs.Data;
using SCA.Service.Inputs.Services;
using SCA.Shared.Startup;

namespace SCA.Service.Inputs
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
            services.AddControllers();

            ScaStartup.AddDbContext<InputsContext>(services, Configuration.GetConnectionString("InputsContext"), "SCA.Service.Inputs");

            ScaStartup.AddAuthentication(services);

            services.AddScoped<SeedingService>();
            services.AddScoped<InsumoService>();
            services.AddScoped<MarcaService>();
            services.AddScoped<TipoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
