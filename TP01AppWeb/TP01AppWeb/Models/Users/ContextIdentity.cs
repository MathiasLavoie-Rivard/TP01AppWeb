using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace TP01AppWeb.Models.Users
{
    public sealed class ContextIdentity : IdentityDbContext<IdentityUser>, ReadMe
    {

        public ContextIdentity(DbContextOptions<ContextIdentity> options)
            : base(options) {
        }
    }
}