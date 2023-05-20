using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class SectionDocumentCategoryRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionBase_CategoryBase_CategoryBaseId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_SectionBase_CategoryBaseId",
                table: "SectionBase");

            migrationBuilder.DropColumn(
                name: "CategoryBaseId",
                table: "SectionBase");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "SectionBase",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "SectionBase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryBaseId",
                table: "SectionBase",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_CategoryBaseId",
                table: "SectionBase",
                column: "CategoryBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionBase_CategoryBase_CategoryBaseId",
                table: "SectionBase",
                column: "CategoryBaseId",
                principalTable: "CategoryBase",
                principalColumn: "Id");
        }
    }
}
