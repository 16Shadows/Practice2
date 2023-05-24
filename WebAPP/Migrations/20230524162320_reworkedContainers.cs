using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class reworkedContainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerDMOPageDMO");

            migrationBuilder.AddColumn<int>(
                name: "ParentPageId",
                table: "Containers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ParentPageId",
                table: "Containers",
                column: "ParentPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Pages_ParentPageId",
                table: "Containers",
                column: "ParentPageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Pages_ParentPageId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ParentPageId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ParentPageId",
                table: "Containers");

            migrationBuilder.CreateTable(
                name: "ContainerDMOPageDMO",
                columns: table => new
                {
                    ContainerDMOsID = table.Column<int>(type: "INTEGER", nullable: false),
                    PageDMOsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerDMOPageDMO", x => new { x.ContainerDMOsID, x.PageDMOsId });
                    table.ForeignKey(
                        name: "FK_ContainerDMOPageDMO_Containers_ContainerDMOsID",
                        column: x => x.ContainerDMOsID,
                        principalTable: "Containers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerDMOPageDMO_Pages_PageDMOsId",
                        column: x => x.PageDMOsId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerDMOPageDMO_PageDMOsId",
                table: "ContainerDMOPageDMO",
                column: "PageDMOsId");
        }
    }
}
