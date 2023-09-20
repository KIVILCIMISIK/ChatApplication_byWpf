namespace ChatApplication_byWpf
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "User_name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "User_name");
            
        }
        
        public override void Down()
        {
            
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "User_name", c => c.String());
            AddPrimaryKey("dbo.Users", "User_name");
        }
    }
}
