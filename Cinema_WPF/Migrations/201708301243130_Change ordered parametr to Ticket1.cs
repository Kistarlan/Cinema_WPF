namespace Cinema_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeorderedparametrtoTicket1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "Ordered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "Ordered", c => c.Boolean(nullable: false));
        }
    }
}
