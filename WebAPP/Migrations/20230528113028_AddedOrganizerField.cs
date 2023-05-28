using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrganizerField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_CategoryBase_ParentOrganizerId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ParentOrganizerId",
                table: "Books",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_ParentOrganizerId",
                table: "Books",
                newName: "IX_Books_OrganizerId");

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Pages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Objects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Containers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_OrganizerId",
                table: "Pages",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_OrganizerId",
                table: "Objects",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_OrganizerId",
                table: "Containers",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_CategoryBase_OrganizerId",
                table: "Books",
                column: "OrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_CategoryBase_OrganizerId",
                table: "Containers",
                column: "OrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_CategoryBase_OrganizerId",
                table: "Objects",
                column: "OrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_CategoryBase_OrganizerId",
                table: "Pages",
                column: "OrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_CategoryBase_OrganizerId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_CategoryBase_OrganizerId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Objects_CategoryBase_OrganizerId",
                table: "Objects");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_CategoryBase_OrganizerId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_OrganizerId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Objects_OrganizerId",
                table: "Objects");

            migrationBuilder.DropIndex(
                name: "IX_Containers_OrganizerId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Containers");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Books",
                newName: "ParentOrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_OrganizerId",
                table: "Books",
                newName: "IX_Books_ParentOrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_CategoryBase_ParentOrganizerId",
                table: "Books",
                column: "ParentOrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
