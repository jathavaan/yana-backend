using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yana.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citations_Users_UserProfileId",
                table: "Citations");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentHasUser_Users_UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalUserProfile_Users_UserId",
                table: "ExternalUserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Users_UserProfileId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TileHasUser_Users_UserProfileId",
                table: "TileHasUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalUserProfile",
                table: "ExternalUserProfile");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserProfiles");

            migrationBuilder.RenameTable(
                name: "ExternalUserProfile",
                newName: "ExternalUserProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalUserProfile_UserId",
                table: "ExternalUserProfiles",
                newName: "IX_ExternalUserProfiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalUserProfiles",
                table: "ExternalUserProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Citations_UserProfiles_UserProfileId",
                table: "Citations",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentHasUser_UserProfiles_UserProfileId",
                table: "DocumentHasUser",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalUserProfiles_UserProfiles_UserId",
                table: "ExternalUserProfiles",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_UserProfiles_UserProfileId",
                table: "Tags",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TileHasUser_UserProfiles_UserProfileId",
                table: "TileHasUser",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citations_UserProfiles_UserProfileId",
                table: "Citations");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentHasUser_UserProfiles_UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalUserProfiles_UserProfiles_UserId",
                table: "ExternalUserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_UserProfiles_UserProfileId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TileHasUser_UserProfiles_UserProfileId",
                table: "TileHasUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalUserProfiles",
                table: "ExternalUserProfiles");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ExternalUserProfiles",
                newName: "ExternalUserProfile");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalUserProfiles_UserId",
                table: "ExternalUserProfile",
                newName: "IX_ExternalUserProfile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalUserProfile",
                table: "ExternalUserProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Citations_Users_UserProfileId",
                table: "Citations",
                column: "UserProfileId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentHasUser_Users_UserProfileId",
                table: "DocumentHasUser",
                column: "UserProfileId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalUserProfile_Users_UserId",
                table: "ExternalUserProfile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Users_UserProfileId",
                table: "Tags",
                column: "UserProfileId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TileHasUser_Users_UserProfileId",
                table: "TileHasUser",
                column: "UserProfileId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
