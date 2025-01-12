using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yana.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLastEditedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citations_Users_UserId",
                table: "Citations");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentHasUser_Users_UserId",
                table: "DocumentHasUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Users_UserId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TileHasUser_Users_UserId",
                table: "TileHasUser");

            migrationBuilder.DropIndex(
                name: "IX_TileHasUser_UserId",
                table: "TileHasUser");

            migrationBuilder.DropIndex(
                name: "IX_DocumentHasUser_UserId",
                table: "DocumentHasUser");

            migrationBuilder.DropColumn(
                name: "LastLogindDate",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tags",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                newName: "IX_Tags_UserProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Citations",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Citations_UserId",
                table: "Citations",
                newName: "IX_Citations_UserProfileId");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileId",
                table: "TileHasUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileId",
                table: "DocumentHasUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TileHasUser_UserProfileId",
                table: "TileHasUser",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHasUser_UserProfileId",
                table: "DocumentHasUser",
                column: "UserProfileId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citations_Users_UserProfileId",
                table: "Citations");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentHasUser_Users_UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Users_UserProfileId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TileHasUser_Users_UserProfileId",
                table: "TileHasUser");

            migrationBuilder.DropIndex(
                name: "IX_TileHasUser_UserProfileId",
                table: "TileHasUser");

            migrationBuilder.DropIndex(
                name: "IX_DocumentHasUser_UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "TileHasUser");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "DocumentHasUser");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Tags",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_UserProfileId",
                table: "Tags",
                newName: "IX_Tags_UserId");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Citations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Citations_UserProfileId",
                table: "Citations",
                newName: "IX_Citations_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogindDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TileHasUser_UserId",
                table: "TileHasUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHasUser_UserId",
                table: "DocumentHasUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citations_Users_UserId",
                table: "Citations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentHasUser_Users_UserId",
                table: "DocumentHasUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Users_UserId",
                table: "Tags",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TileHasUser_Users_UserId",
                table: "TileHasUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
