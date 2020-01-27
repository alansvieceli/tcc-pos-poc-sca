using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SCA.Service.Auth.Providers;
using SCA.Service.Auth.Services;
using SCA.Service.Inputs.Data;
using SCA.Shared.Extensions;

namespace SCA.Service.Auth
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
            services.AddCors();

            services.AddControllers();

            services.AddContexto<AuthContext>(Configuration.GetConnectionString("AuthContext"), "SCA.Service.Auth");

            services.AddScoped<SeedingService>();
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<TokenProvider>();

            services.AddSwaggerDocumentation("SCA.Service.Auth");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }

            seedingService.Seed();

            app.UseSwagger();

            app.UseSwaggerDocumentation("SCA.Service.Auth v1.0");

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
