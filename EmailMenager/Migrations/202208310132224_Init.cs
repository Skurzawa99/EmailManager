namespace EmailMenager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emails", "FileUpload_Id", "dbo.FileUploads");
            DropIndex("dbo.Emails", new[] { "FileUpload_Id" });
            AddColumn("dbo.Emails", "FileName", c => c.String());
            AddColumn("dbo.Emails", "FileUpload", c => c.Binary());
            DropColumn("dbo.Emails", "FileUpload_Id");
            DropTable("dbo.FileUploads");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileUploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        File = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Emails", "FileUpload_Id", c => c.Int());
            DropColumn("dbo.Emails", "FileUpload");
            DropColumn("dbo.Emails", "FileName");
            CreateIndex("dbo.Emails", "FileUpload_Id");
            AddForeignKey("dbo.Emails", "FileUpload_Id", "dbo.FileUploads", "Id");
        }
    }
}
