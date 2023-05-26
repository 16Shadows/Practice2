using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class BookParentsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentOrganizerId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ParentOrganizerId",
                table: "Books",
                column: "ParentOrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_CategoryBase_ParentOrganizerId",
                table: "Books",
                column: "ParentOrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_CategoryBase_ParentOrganizerId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ParentOrganizerId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ParentOrganizerId",
                table: "Books");
        }
    }
}
