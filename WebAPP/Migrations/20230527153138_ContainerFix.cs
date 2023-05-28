using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class ContainerFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerDMOObjectDMO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Containers",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ParentContainerId",
                table: "Objects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Objects_ParentContainerId",
                table: "Objects",
                column: "ParentContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_Containers_ParentContainerId",
                table: "Objects",
                column: "ParentContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_Containers_ParentContainerId",
                table: "Objects");

            migrationBuilder.DropIndex(
                name: "IX_Objects_ParentContainerId",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "ParentContainerId",
                table: "Objects");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Containers",
                newName: "ID");

            migrationBuilder.CreateTable(
                name: "ContainerDMOObjectDMO",
                columns: table => new
                {
                    ContainerDMOsID = table.Column<int>(type: "INTEGER", nullable: false),
                    ObjectDMOsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerDMOObjectDMO", x => new { x.ContainerDMOsID, x.ObjectDMOsId });
                    table.ForeignKey(
                        name: "FK_ContainerDMOObjectDMO_Containers_ContainerDMOsID",
                        column: x => x.ContainerDMOsID,
                        principalTable: "Containers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerDMOObjectDMO_Objects_ObjectDMOsId",
                        column: x => x.ObjectDMOsId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerDMOObjectDMO_ObjectDMOsId",
                table: "ContainerDMOObjectDMO",
                column: "ObjectDMOsId");
        }
    }
}
