namespace MVVMTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PermissionName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TblRules",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RuleName = c.String(),
                        RuleNote = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TblUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        UserName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.Long(nullable: false),
                        UpdatedAt = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        InRelation = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TblRuleTblPermissions",
                c => new
                    {
                        TblRule_Id = c.Long(nullable: false),
                        TblPermission_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TblRule_Id, t.TblPermission_Id })
                .ForeignKey(
                    "dbo.TblRules", 
                    t => t.TblRule_Id, 
                    cascadeDelete: true)
                .ForeignKey(
                    "dbo.TblPermissions", 
                    t => t.TblPermission_Id, 
                    cascadeDelete: true)
                .Index(t => t.TblRule_Id)
                .Index(t => t.TblPermission_Id);
            
            CreateTable(
                "dbo.TblUserTblRules",
                c => new
                    {
                        TblUser_Id = c.Long(nullable: false),
                        TblRule_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TblUser_Id, t.TblRule_Id })
                .ForeignKey("dbo.TblUsers", 
                    t => t.TblUser_Id, 
                    cascadeDelete: true)
                .ForeignKey("dbo.TblRules", 
                    t => t.TblRule_Id,
                    cascadeDelete: true)
                .Index(t => t.TblUser_Id)
                .Index(t => t.TblRule_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblUserTblRules", "TblRule_Id", "dbo.TblRules");
            DropForeignKey("dbo.TblUserTblRules", "TblUser_Id", "dbo.TblUsers");
            DropForeignKey("dbo.TblRuleTblPermissions", "TblPermission_Id", "dbo.TblPermissions");
            DropForeignKey("dbo.TblRuleTblPermissions", "TblRule_Id", "dbo.TblRules");
            DropIndex("dbo.TblUserTblRules", new[] { "TblRule_Id" });
            DropIndex("dbo.TblUserTblRules", new[] { "TblUser_Id" });
            DropIndex("dbo.TblRuleTblPermissions", new[] { "TblPermission_Id" });
            DropIndex("dbo.TblRuleTblPermissions", new[] { "TblRule_Id" });
            DropTable("dbo.TblUserTblRules");
            DropTable("dbo.TblRuleTblPermissions");
            DropTable("dbo.People");
            DropTable("dbo.TblUsers");
            DropTable("dbo.TblRules");
            DropTable("dbo.TblPermissions");
        }
    }
}
