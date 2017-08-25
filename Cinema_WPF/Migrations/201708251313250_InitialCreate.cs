namespace Cinema_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Desription = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        FilmId = c.Int(),
                        HallId = c.Int(),
                        Films_Id = c.Int(),
                        Halls_Id = c.Int(),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Films_Id)
                .ForeignKey("dbo.Films", t => t.Halls_Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .ForeignKey("dbo.Halls", t => t.HallId)
                .Index(t => t.HallId)
                .Index(t => t.Films_Id)
                .Index(t => t.Halls_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        SessionId = c.Int(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.SessionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Login = c.String(),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        UserRoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        RowCount = c.Int(nullable: false),
                        ColumnCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FilmDirectors",
                c => new
                    {
                        Film_Id = c.Int(nullable: false),
                        Director_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_Id, t.Director_Id })
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .ForeignKey("dbo.Directors", t => t.Director_Id, cascadeDelete: true)
                .Index(t => t.Film_Id)
                .Index(t => t.Director_Id);
            
            CreateTable(
                "dbo.GenreFilms",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Film_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Film_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Sessions", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.Sessions", "Halls_Id", "dbo.Films");
            DropForeignKey("dbo.Sessions", "Films_Id", "dbo.Films");
            DropForeignKey("dbo.GenreFilms", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.GenreFilms", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.FilmDirectors", "Director_Id", "dbo.Directors");
            DropForeignKey("dbo.FilmDirectors", "Film_Id", "dbo.Films");
            DropIndex("dbo.GenreFilms", new[] { "Film_Id" });
            DropIndex("dbo.GenreFilms", new[] { "Genre_Id" });
            DropIndex("dbo.FilmDirectors", new[] { "Director_Id" });
            DropIndex("dbo.FilmDirectors", new[] { "Film_Id" });
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "SessionId" });
            DropIndex("dbo.Sessions", new[] { "Film_Id" });
            DropIndex("dbo.Sessions", new[] { "Halls_Id" });
            DropIndex("dbo.Sessions", new[] { "Films_Id" });
            DropIndex("dbo.Sessions", new[] { "HallId" });
            DropTable("dbo.GenreFilms");
            DropTable("dbo.FilmDirectors");
            DropTable("dbo.Halls");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
            DropTable("dbo.Sessions");
            DropTable("dbo.Genres");
            DropTable("dbo.Films");
            DropTable("dbo.Directors");
        }
    }
}
