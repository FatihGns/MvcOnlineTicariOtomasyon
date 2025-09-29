namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameAlici : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Alici", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.Messages", "Alıcı");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Alıcı", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.Messages", "Alici");
        }
    }
}
