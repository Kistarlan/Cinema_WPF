namespace Cinema_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderedparametrtoTicket : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "Ordered", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Tickets", "Ordered", c => c.Boolean(nullable: false));
        }
    }
}
