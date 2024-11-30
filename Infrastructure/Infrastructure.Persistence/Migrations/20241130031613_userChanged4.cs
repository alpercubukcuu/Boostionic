using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userChanged4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OwnerId",
                table: "Users",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OwnersEntities_OwnerId",
                table: "Users",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_OwnersEntities_OwnerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OwnerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Users");
        }
    }
}
