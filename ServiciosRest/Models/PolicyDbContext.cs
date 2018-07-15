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

        static PolicyDbContext policyDbContext = new PolicyDbContext();
        public PolicyDbContext()
            : base(string.Format("name={0}", "PolicyConnection"))
        {
            //Create();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        }

        public static PolicyDbContext Create()
        {
            return new PolicyDbContext();
           
        }
    }
}
