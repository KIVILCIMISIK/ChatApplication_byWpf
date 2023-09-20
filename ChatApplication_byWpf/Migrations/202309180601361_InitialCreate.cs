namespace ChatApplication_byWpf
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        
                        User_name = c.String(),
                        Login_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.User_name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
