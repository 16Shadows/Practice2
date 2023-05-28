using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class Madetagsuniqueperorganizer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Tags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name_OrganizerId",
                table: "Tags",
                columns: new[] { "Name", "OrganizerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_OrganizerId",
                table: "Tags",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_CategoryBase_OrganizerId",
                table: "Tags",
                column: "OrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_CategoryBase_OrganizerId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Name_OrganizerId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_OrganizerId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Tags");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);
        }
    }
}
