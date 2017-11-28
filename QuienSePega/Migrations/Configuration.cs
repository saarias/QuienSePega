namespace QuienSePega.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuienSePega.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "QuienSePega.Models.ApplicationDbContext";
        }

        protected override void Seed(QuienSePega.Models.ApplicationDbContext context)
        {
            context.Lugares.AddOrUpdate(
                new Models.Lugares { Latitud = "4.620985", Longitud = "-74.072741",Nombre = "Plazoleta", Tipo = "ORIGEN"},
                new Models.Lugares { Latitud = "4.617723", Longitud = "-74.073235", Nombre = "Sede Wayu", Tipo = "ORIGEN" },
                new Models.Lugares { Latitud = "4.620472", Longitud = "-74.070263", Nombre = "Av Caracas", Tipo = "DESTINO" },
                new Models.Lugares { Latitud = "4.619884", Longitud = "-74.075585", Nombre = "Calle 26", Tipo = "DESTINO" }
            );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
