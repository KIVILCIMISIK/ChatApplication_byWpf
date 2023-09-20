namespace ChatApplication_byWpf
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        User_name = c.String(nullable: false, maxLength: 128),
                        Message_text = c.String(),
                        Message_time = c.DateTime(nullable: false),
                        Last_Message_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.User_name);
            
            AlterColumn("dbo.Users", "User_name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "User_name");
            DropColumn("dbo.Users", "User_name");
        }
        
        public override void Down()
        {
            
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "User_name", c => c.String());
            DropTable("dbo.Messages");
            AddPrimaryKey("dbo.Users", "User_name");
        }
    }
}
