namespace Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Policies", "CoverageType_Id", "dbo.CoverageTypes");
            DropIndex("dbo.Policies", new[] { "CoverageType_Id" });
            RenameColumn(table: "dbo.Policies", name: "CoverageType_Id", newName: "CoverageTypeId");
            AlterColumn("dbo.Policies", "CoverageTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Policies", "CoverageTypeId");
            AddForeignKey("dbo.Policies", "CoverageTypeId", "dbo.CoverageTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Policies", "CoverageTypeId", "dbo.CoverageTypes");
            DropIndex("dbo.Policies", new[] { "CoverageTypeId" });
            AlterColumn("dbo.Policies", "CoverageTypeId", c => c.Int());
            RenameColumn(table: "dbo.Policies", name: "CoverageTypeId", newName: "CoverageType_Id");
            CreateIndex("dbo.Policies", "CoverageType_Id");
            AddForeignKey("dbo.Policies", "CoverageType_Id", "dbo.CoverageTypes", "Id");
        }
    }
}
