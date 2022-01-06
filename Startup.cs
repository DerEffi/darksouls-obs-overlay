using DarkSoulsOBSOverlay.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;

namespace DarkSoulsOBSOverlay
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

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Frontend/build";
            });

            // Custom DarkSouls Background service for reading data
            DarkSoulsReader.Timer.Elapsed += (object source, ElapsedEventArgs e) => { DarkSoulsReader.SendDarkSoulsData(); };
            DarkSoulsReader.Timer.Start();

            // Load Settings from file if exists
            DarkSoulsReader.SetSettings(FileService.LoadSettings());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseWebSockets();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Frontend";

#if DEBUG
                spa.UseReactDevelopmentServer(npmScript: "start");
#endif
            });
        }
    }
}
