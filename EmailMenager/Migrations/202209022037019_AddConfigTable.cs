namespace EmailMenager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfigTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        PasswordEmail = c.String(nullable: false),
                        Host = c.String(nullable: false),
                        Port = c.Int(nullable: false),
                        EnableSsl = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Emails", "Config_Id", c => c.Int());
            CreateIndex("dbo.Emails", "Config_Id");
            AddForeignKey("dbo.Emails", "Config_Id", "dbo.Configs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "Config_Id", "dbo.Configs");
            DropIndex("dbo.Emails", new[] { "Config_Id" });
            DropColumn("dbo.Emails", "Config_Id");
            DropTable("dbo.Configs");
        }
    }
}
