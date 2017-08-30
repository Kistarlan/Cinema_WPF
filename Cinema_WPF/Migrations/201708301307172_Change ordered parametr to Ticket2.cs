namespace Cinema_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeorderedparametrtoTicket2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Ordered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Ordered");
        }
    }
}
