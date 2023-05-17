using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class DirectOrganizerReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryBase_AspNetUsers_OwnerId1",
                table: "CategoryBase");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionBase_CategoryBase_Document_ParentId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_SectionBase_Document_ParentId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_SectionBase_Title_Document_ParentId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_CategoryBase_OwnerId1",
                table: "CategoryBase");

            migrationBuilder.DropColumn(
                name: "Document_ParentId",
                table: "SectionBase");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "CategoryBase");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SectionBase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "SectionBase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "CategoryBase",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "CategoryBase",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_CategoryId",
                table: "SectionBase",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_OrganizerId",
                table: "SectionBase",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_Title_CategoryId",
                table: "SectionBase",
                columns: new[] { "Title", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBase_OrganizerId",
                table: "CategoryBase",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryBase_AspNetUsers_OwnerId",
                table: "CategoryBase",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryBase_CategoryBase_OrganizerId",
                table: "CategoryBase",
                column: "OrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionBase_CategoryBase_CategoryId",
                table: "SectionBase",
                column: "CategoryId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionBase_CategoryBase_OrganizerId",
                table: "SectionBase",
                column: "OrganizerId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryBase_AspNetUsers_OwnerId",
                table: "CategoryBase");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryBase_CategoryBase_OrganizerId",
                table: "CategoryBase");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionBase_CategoryBase_CategoryId",
                table: "SectionBase");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionBase_CategoryBase_OrganizerId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_SectionBase_CategoryId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_SectionBase_OrganizerId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_SectionBase_Title_CategoryId",
                table: "SectionBase");

            migrationBuilder.DropIndex(
                name: "IX_CategoryBase_OrganizerId",
                table: "CategoryBase");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SectionBase");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "SectionBase");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "CategoryBase");

            migrationBuilder.AddColumn<int>(
                name: "Document_ParentId",
                table: "SectionBase",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "CategoryBase",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "CategoryBase",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_Document_ParentId",
                table: "SectionBase",
                column: "Document_ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_Title_Document_ParentId",
                table: "SectionBase",
                columns: new[] { "Title", "Document_ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBase_OwnerId1",
                table: "CategoryBase",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryBase_AspNetUsers_OwnerId1",
                table: "CategoryBase",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionBase_CategoryBase_Document_ParentId",
                table: "SectionBase",
                column: "Document_ParentId",
                principalTable: "CategoryBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
