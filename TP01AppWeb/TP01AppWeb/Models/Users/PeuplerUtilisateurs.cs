using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Users
{
    public class PeuplerUtilisateurs : ReadMe
    {
        public static void CréerCompteAdmin(IServiceProvider p_serviceProvider,
                                            IConfiguration p_configuration)
        {
            CreateRoles(p_serviceProvider, p_configuration).Wait();
            CréerCompteAdminAsync(p_serviceProvider, p_configuration).Wait();
        }
        public static async Task CréerCompteAdminAsync(IServiceProvider p_serviceProvider,
                                                       IConfiguration p_configuration)
        {
            p_serviceProvider = p_serviceProvider.CreateScope().ServiceProvider;
            UserManager<IdentityUser> gestionnaireUtilisateur =
            p_serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> gestionnaireRôle =
            p_serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string nomUtilisateur = p_configuration["Données:UtilsateurAdmin:Nom"] ?? "AdminI";
            string MDP = p_configuration["Données:UtilsateurAdmin:MDP"] ?? "InimdA23";
            string rôle = p_configuration["Données:UtilsateurAdmin:Rôle"] ?? "Admin";
            if (await gestionnaireUtilisateur.FindByNameAsync(nomUtilisateur) == null)
            {
                if (await gestionnaireRôle.FindByNameAsync(rôle) == null)
                {
                    await gestionnaireRôle.CreateAsync(new IdentityRole(rôle));
                }
                IdentityUser utilisateur = new IdentityUser
                {
                    UserName = nomUtilisateur,
                };
                IdentityResult résultat = await gestionnaireUtilisateur
                .CreateAsync(utilisateur, MDP);
                if (résultat.Succeeded)
                {
                    await gestionnaireUtilisateur.AddToRoleAsync(utilisateur, rôle);
                }
            }
        }

        private static async Task CreateRoles(IServiceProvider p_serviceProvider,
                                                       IConfiguration p_configuration)

        {
            p_serviceProvider = p_serviceProvider.CreateScope().ServiceProvider;
            RoleManager<IdentityRole> gestionnaireRôle =
            p_serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string RoleAdmin = "Admin";
            string RoleGerant = "Gérant";
            string RoleCommis = "Commis";

            if (await gestionnaireRôle.FindByNameAsync(RoleAdmin) == null)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = RoleAdmin
                };
                await gestionnaireRôle.CreateAsync(newRole);
            }

            if (await gestionnaireRôle.FindByNameAsync(RoleGerant) == null)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = RoleGerant
                };
                await gestionnaireRôle.CreateAsync(newRole);
            }

            if (await gestionnaireRôle.FindByNameAsync(RoleCommis) == null)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = RoleCommis
                };
                await gestionnaireRôle.CreateAsync(newRole);
            }


        }

    }
}
