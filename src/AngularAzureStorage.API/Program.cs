using AngularAzureStorage.Core;
using AngularAzureStorage.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AngularAzureStorage.API
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateWebHostBuilder().Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();        
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddHttpContextAccessor();

            services.AddCustomSignalR()
                .AddCustomSwagger()
                .AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app)
        {            
            app.UseAuthentication()
                .UseMvc()
                .UseCors(CorsDefaults.Policy)
                .UseSignalR(routes => routes.MapHub<IntegrationEventsHub>("/hub"))
                .UseSwagger()
                .UseSwaggerUI(options
                =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Angular Azure Storage API");
                    options.RoutePrefix = string.Empty;
                });
        }
    }
}
