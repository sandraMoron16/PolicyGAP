namespace Common.Migrations
{
    using Common.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Common.Data.PolicyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Common.Data.PolicyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
           
            context.Clients.AddOrUpdate(x => x.Id,
                                        new Client() { Id = 1, Name = "Jane Austen" },
                                        new Client() { Id = 2, Name = "Charles Dickens" },
                                        new Client() { Id = 3, Name = "Miguel de Cervantes" }
                                       );

            context.CoverageTypes.AddOrUpdate(x => x.Id,
                                     new CoverageType() { Id = 1, Name = "Terremoto" },
                                     new CoverageType() { Id = 2, Name = "Incendio" },
                                     new CoverageType() { Id = 3, Name = "Robo" },
                                       new CoverageType() { Id = 2, Name = "Pérdida" }
                                    );
        }
    }
}
