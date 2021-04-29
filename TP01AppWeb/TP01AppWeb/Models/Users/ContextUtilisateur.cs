using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace TP01AppWeb.Models
{
    public sealed class ContextUtilisateur : DbContext
    {

        public ContextUtilisateur(DbContextOptions<ContextUtilisateur> options)
            : base(options) { }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
