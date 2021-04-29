using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TP01AppWeb.Models.Entreprise;
using TP01AppWeb.Models.Users;

namespace TP01AppWeb
{
    public class Startup : ReadMe
    {

        public Startup(IConfiguration p_config)
        {
            Configuration = p_config;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextEntreprise>(opts =>
            {
                opts.UseSqlServer(Configuration[
                "ChainesConnexion:ConnexionEntreprise"]);
                opts.EnableSensitiveDataLogging(true);
            });

            services.AddDbContext<ContextUtilisateur>(opts =>
            {
                opts.UseSqlServer(Configuration[
                "ChainesConnexion:ConnexionUtilisateur"]);
                opts.EnableSensitiveDataLogging(true);
            });

            services.AddSingleton<IDepot, DepotEF>();
            services.AddControllersWithViews();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ContextUtilisateur contextU, ContextEntreprise contextE)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
