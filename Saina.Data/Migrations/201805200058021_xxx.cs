namespace Saina.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxx : DbMigration
    {
        public override void Up()
        {
            AddColumn("Acc.AccDocumentItems", "HasExchange", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Acc.AccDocumentItems", "HasExchange");
        }
    }
}
