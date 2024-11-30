using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_OwnersEntities_OwnerId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_OwnersEntities_OwnerId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Industries_OwnersEntities_OwnerId",
                table: "Industries");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_OwnersEntities_OwnerId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_OwnersEntities_OwnerId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCategories_OwnersEntities_OwnerId",
                table: "ProjectCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_OwnersEntities_OwnerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStages_OwnersEntities_OwnerId",
                table: "ProjectStages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_OwnersEntities_OwnerId",
                table: "ProjectTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesPermissions_OwnersEntities_OwnerId",
                table: "RolesPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_OwnersEntities_OwnerId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserResetPasswords_OwnersEntities_OwnerId",
                table: "UserResetPasswords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_OwnersEntities_OwnerId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_OwnersEntities_OwnerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OwnerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_OwnerId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserResetPasswords_OwnerId",
                table: "UserResetPasswords");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_OwnerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_RolesPermissions_OwnerId",
                table: "RolesPermissions");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_OwnerId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectStages_OwnerId",
                table: "ProjectStages");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectCategories_OwnerId",
                table: "ProjectCategories");

            migrationBuilder.DropIndex(
                name: "IX_Logs_OwnerId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Languages_OwnerId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Industries_OwnerId",
                table: "Industries");

            migrationBuilder.DropIndex(
                name: "IX_Emails_OwnerId",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Clients_OwnerId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "UserResetPasswords");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "RolesPermissions");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ProjectStages");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ProjectCategories");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Industries");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Clients");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "UserType",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "UserRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "UserResetPasswords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "RolesPermissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ProjectTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ProjectStages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ProjectCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Languages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Industries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Emails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_OwnerId",
                table: "Users",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_OwnerId",
                table: "UserRoles",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResetPasswords_OwnerId",
                table: "UserResetPasswords",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OwnerId",
                table: "Tickets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_OwnerId",
                table: "RolesPermissions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_OwnerId",
                table: "ProjectTasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStages_OwnerId",
                table: "ProjectStages",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategories_OwnerId",
                table: "ProjectCategories",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_OwnerId",
                table: "Logs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_OwnerId",
                table: "Languages",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Industries_OwnerId",
                table: "Industries",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_OwnerId",
                table: "Emails",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_OwnerId",
                table: "Clients",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_OwnersEntities_OwnerId",
                table: "Clients",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_OwnersEntities_OwnerId",
                table: "Emails",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Industries_OwnersEntities_OwnerId",
                table: "Industries",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_OwnersEntities_OwnerId",
                table: "Languages",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_OwnersEntities_OwnerId",
                table: "Logs",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCategories_OwnersEntities_OwnerId",
                table: "ProjectCategories",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_OwnersEntities_OwnerId",
                table: "Projects",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStages_OwnersEntities_OwnerId",
                table: "ProjectStages",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_OwnersEntities_OwnerId",
                table: "ProjectTasks",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesPermissions_OwnersEntities_OwnerId",
                table: "RolesPermissions",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_OwnersEntities_OwnerId",
                table: "Tickets",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserResetPasswords_OwnersEntities_OwnerId",
                table: "UserResetPasswords",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_OwnersEntities_OwnerId",
                table: "UserRoles",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OwnersEntities_OwnerId",
                table: "Users",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
