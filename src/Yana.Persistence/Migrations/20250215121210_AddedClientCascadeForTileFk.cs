using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yana.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedClientCascadeForTileFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tiles_Documents_DocumentId",
                table: "Tiles");

            migrationBuilder.AddForeignKey(
                name: "FK_Tiles_Documents_DocumentId",
                table: "Tiles",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tiles_Documents_DocumentId",
                table: "Tiles");

            migrationBuilder.AddForeignKey(
                name: "FK_Tiles_Documents_DocumentId",
                table: "Tiles",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
