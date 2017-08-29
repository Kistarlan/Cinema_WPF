namespace Cinema_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Ticket_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Exist", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Exist");
        }
    }
}
