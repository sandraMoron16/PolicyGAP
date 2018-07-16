namespace Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignmentPolicies", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Policies", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignmentPolicies", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.AssignmentPolicies", "Policy_Id", "dbo.Policies");
            DropIndex("dbo.AssignmentPolicies", new[] { "ApplicationUserId" });
            DropIndex("dbo.AssignmentPolicies", new[] { "Client_Id" });
            DropIndex("dbo.AssignmentPolicies", new[] { "Policy_Id" });
            DropIndex("dbo.Policies", new[] { "ApplicationUserId" });
            RenameColumn(table: "dbo.AssignmentPolicies", name: "Client_Id", newName: "ClientId");
            RenameColumn(table: "dbo.AssignmentPolicies", name: "Policy_Id", newName: "PolicyId");
            AlterColumn("dbo.AssignmentPolicies", "ClientId", c => c.Int(nullable: false));
            AlterColumn("dbo.AssignmentPolicies", "PolicyId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignmentPolicies", "PolicyId");
            CreateIndex("dbo.AssignmentPolicies", "ClientId");
            AddForeignKey("dbo.AssignmentPolicies", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AssignmentPolicies", "PolicyId", "dbo.Policies", "Id", cascadeDelete: true);
            DropColumn("dbo.AssignmentPolicies", "ApplicationUserId");
            DropColumn("dbo.Policies", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Policies", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.AssignmentPolicies", "ApplicationUserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AssignmentPolicies", "PolicyId", "dbo.Policies");
            DropForeignKey("dbo.AssignmentPolicies", "ClientId", "dbo.Clients");
            DropIndex("dbo.AssignmentPolicies", new[] { "ClientId" });
            DropIndex("dbo.AssignmentPolicies", new[] { "PolicyId" });
            AlterColumn("dbo.AssignmentPolicies", "PolicyId", c => c.Int());
            AlterColumn("dbo.AssignmentPolicies", "ClientId", c => c.Int());
            RenameColumn(table: "dbo.AssignmentPolicies", name: "PolicyId", newName: "Policy_Id");
            RenameColumn(table: "dbo.AssignmentPolicies", name: "ClientId", newName: "Client_Id");
            CreateIndex("dbo.Policies", "ApplicationUserId");
            CreateIndex("dbo.AssignmentPolicies", "Policy_Id");
            CreateIndex("dbo.AssignmentPolicies", "Client_Id");
            CreateIndex("dbo.AssignmentPolicies", "ApplicationUserId");
            AddForeignKey("dbo.AssignmentPolicies", "Policy_Id", "dbo.Policies", "Id");
            AddForeignKey("dbo.AssignmentPolicies", "Client_Id", "dbo.Clients", "Id");
            AddForeignKey("dbo.Policies", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AssignmentPolicies", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
