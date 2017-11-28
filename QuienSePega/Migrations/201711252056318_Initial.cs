namespace QuienSePega.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Eventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Creador = c.String(),
                        IdLugarOrigen = c.Int(nullable: false),
                        IdLugarDestino = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IntegrantesEventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEvento = c.Int(nullable: false),
                        Integrante = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lugares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitud = c.String(),
                        Longitud = c.String(),
                        Tipo = c.String(),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Lugares");
            DropTable("dbo.IntegrantesEventoes");
            DropTable("dbo.Eventoes");
        }
    }
}
