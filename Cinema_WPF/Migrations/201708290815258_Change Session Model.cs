namespace Cinema_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSessionModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sessions", "Films_Id", "dbo.Films");
            DropForeignKey("dbo.Sessions", "Halls_Id", "dbo.Films");
            DropIndex("dbo.Sessions", new[] { "Films_Id" });
            DropIndex("dbo.Sessions", new[] { "Halls_Id" });
            DropColumn("dbo.Sessions", "FilmId");
            RenameColumn(table: "dbo.Sessions", name: "Film_Id", newName: "FilmId");
            RenameIndex(table: "dbo.Sessions", name: "IX_Film_Id", newName: "IX_FilmId");
            DropColumn("dbo.Sessions", "Films_Id");
            DropColumn("dbo.Sessions", "Halls_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "Halls_Id", c => c.Int());
            AddColumn("dbo.Sessions", "Films_Id", c => c.Int());
            RenameIndex(table: "dbo.Sessions", name: "IX_FilmId", newName: "IX_Film_Id");
            RenameColumn(table: "dbo.Sessions", name: "FilmId", newName: "Film_Id");
            AddColumn("dbo.Sessions", "FilmId", c => c.Int());
            CreateIndex("dbo.Sessions", "Halls_Id");
            CreateIndex("dbo.Sessions", "Films_Id");
            AddForeignKey("dbo.Sessions", "Halls_Id", "dbo.Films", "Id");
            AddForeignKey("dbo.Sessions", "Films_Id", "dbo.Films", "Id");
        }
    }
}
