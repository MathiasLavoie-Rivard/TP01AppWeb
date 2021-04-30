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
using Microsoft.AspNetCore.Identity;

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

            services.AddDbContext<ContextIdentity>(opts =>
            {
                opts.UseSqlServer(Configuration[
                "ChainesConnexion:ConnexionUtilisateur"]);
                opts.EnableSensitiveDataLogging(true);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                // Par défaut a–z, A–Z, et 0–9 et les carctères - , _ @
                // Cette propriété n'est pas une expression rationnelle, donc tous les
                // valide doivent être énumérés explicitement dans la chaîne.
                //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
                // Les cinq (5) propriétés disponibles pour le mot de passe.
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ContextIdentity>();


            services.AddSingleton<IDepot, DepotEF>();
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(options =>
                options.LoginPath = "/Home/Connect");

            services.ConfigureApplicationCookie(options =>
               options.AccessDeniedPath = "/Home/Connect");
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Gestion",
                    pattern: "{controller=Gestion}/{action?}");
            });

            PeuplerUtilisateurs.CréerCompteAdmin(app.ApplicationServices, Configuration);
        }
    }
}
