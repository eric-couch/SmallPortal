using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmallPortal.Models;

namespace SmallPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SmallPortal.Models.Recipient1099> Recipient1099 { get; set; }
        public DbSet<SmallPortal.Models.Recipient> Recipient { get; set; }
        public DbSet<SmallPortal.Models.Boxvalues> Boxvalues { get; set; }
        public DbSet<SmallPortal.Models.Recipient1099InputModel> Recipient1099InputModel { get; set; }
    }
}
