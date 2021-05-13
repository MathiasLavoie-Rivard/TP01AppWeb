using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TP01AppWeb.Models.Entreprise
{
    public sealed class ContextEntreprise : DbContext, ReadMe
    {
        public ContextEntreprise(DbContextOptions<ContextEntreprise> options)
            : base(options) { }
        public DbSet<Succursale> Succursales { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DossierAccident> DossierAccidents { get;set;}
    }
}
