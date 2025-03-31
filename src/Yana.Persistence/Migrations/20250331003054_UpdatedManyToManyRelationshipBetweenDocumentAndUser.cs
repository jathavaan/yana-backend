using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yana.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedManyToManyRelationshipBetweenDocumentAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentHasUser_UserProfiles_UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.DropIndex(
                name: "IX_DocumentHasUser_UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHasUser_UserId",
                table: "DocumentHasUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentHasUser_UserProfiles_UserId",
                table: "DocumentHasUser",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentHasUser_UserProfiles_UserId",
                table: "DocumentHasUser");

            migrationBuilder.DropIndex(
                name: "IX_DocumentHasUser_UserId",
                table: "DocumentHasUser");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileId",
                table: "DocumentHasUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHasUser_UserProfileId",
                table: "DocumentHasUser",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentHasUser_UserProfiles_UserProfileId",
                table: "DocumentHasUser",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
