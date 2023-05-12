using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class ModelRevisions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_Containers_ContainerDMOID",
                table: "Objects");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Objects_ObjectDMOId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_ObjectDMOId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Objects_ContainerDMOID",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "ObjectDMOId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ContainerDMOID",
                table: "Objects");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerDMOObjectDMO");

            migrationBuilder.AddColumn<int>(
                name: "ObjectDMOId",
                table: "Pages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContainerDMOID",
                table: "Objects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ObjectDMOId",
                table: "Pages",
                column: "ObjectDMOId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_ContainerDMOID",
                table: "Objects",
                column: "ContainerDMOID");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_Containers_ContainerDMOID",
                table: "Objects",
                column: "ContainerDMOID",
                principalTable: "Containers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Objects_ObjectDMOId",
                table: "Pages",
                column: "ObjectDMOId",
                principalTable: "Objects",
                principalColumn: "Id");
        }
    }
}
