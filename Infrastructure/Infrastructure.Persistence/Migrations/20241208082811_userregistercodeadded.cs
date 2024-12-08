using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userregistercodeadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessPlaces_OwnersEntities_OwnerId",
                table: "BusinessPlaces");

            migrationBuilder.DropIndex(
                name: "IX_BusinessPlaces_OwnerId",
                table: "BusinessPlaces");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerEntityId",
                table: "BusinessPlaces",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserRegisterCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegisterCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRegisterCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPlaces_OwnerEntityId",
                table: "BusinessPlaces",
                column: "OwnerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegisterCodes_UserId",
                table: "UserRegisterCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessPlaces_OwnersEntities_OwnerEntityId",
                table: "BusinessPlaces",
                column: "OwnerEntityId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessPlaces_OwnersEntities_OwnerEntityId",
                table: "BusinessPlaces");

            migrationBuilder.DropTable(
                name: "UserRegisterCodes");

            migrationBuilder.DropIndex(
                name: "IX_BusinessPlaces_OwnerEntityId",
                table: "BusinessPlaces");

            migrationBuilder.DropColumn(
                name: "OwnerEntityId",
                table: "BusinessPlaces");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPlaces_OwnerId",
                table: "BusinessPlaces",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessPlaces_OwnersEntities_OwnerId",
                table: "BusinessPlaces",
                column: "OwnerId",
                principalTable: "OwnersEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
