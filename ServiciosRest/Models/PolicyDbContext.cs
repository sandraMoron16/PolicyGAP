using Common.Model;
//using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosRest.Models
{
    public  class PolicyDbContext: DbContext
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AssignmentPolicy> AssignmentPolicies { get; set; }
        public DbSet<CoverageType> CoverageTypes { get; set; }

        public PolicyDbContext()
            : base(string.Format("name={0}", "PolicyConnection"))
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
