using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yana.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedExternalUserProfilesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthProvider",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ExternalUserProfile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthProvider = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalUserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalUserProfile_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUserProfile_UserId",
                table: "ExternalUserProfile",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalUserProfile");

            migrationBuilder.AddColumn<int>(
                name: "AuthProvider",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
