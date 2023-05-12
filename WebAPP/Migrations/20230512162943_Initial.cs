using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<uint>(type: "INTEGER", nullable: false),
                    Height = table.Column<uint>(type: "INTEGER", nullable: false),
                    CoordX = table.Column<int>(type: "INTEGER", nullable: false),
                    CoordY = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryBase_Accounts_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryBase_CategoryBase_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CategoryBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LinkToObject = table.Column<string>(type: "TEXT", nullable: false),
                    ContainerDMOID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objects_Containers_ContainerDMOID",
                        column: x => x.ContainerDMOID,
                        principalTable: "Containers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_CategoryBase_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "CategoryBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Document_ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionBase_CategoryBase_Document_ParentId",
                        column: x => x.Document_ParentId,
                        principalTable: "CategoryBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionBase_SectionBase_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SectionBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentBookId = table.Column<int>(type: "INTEGER", nullable: false),
                    ObjectDMOId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Books_ParentBookId",
                        column: x => x.ParentBookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pages_Objects_ObjectDMOId",
                        column: x => x.ObjectDMOId,
                        principalTable: "Objects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTag",
                columns: table => new
                {
                    DocumentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTag", x => new { x.DocumentsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DocumentTag_SectionBase_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "SectionBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Books_ParentCategoryId",
                table: "Books",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBase_Name_ParentId",
                table: "CategoryBase",
                columns: new[] { "Name", "ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBase_OwnerId_Name",
                table: "CategoryBase",
                columns: new[] { "OwnerId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBase_ParentId",
                table: "CategoryBase",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerDMOPageDMO_PageDMOsId",
                table: "ContainerDMOPageDMO",
                column: "PageDMOsId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTag_TagsId",
                table: "DocumentTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_ContainerDMOID",
                table: "Objects",
                column: "ContainerDMOID");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ObjectDMOId",
                table: "Pages",
                column: "ObjectDMOId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ParentBookId",
                table: "Pages",
                column: "ParentBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_Position_ParentBookId",
                table: "Pages",
                columns: new[] { "Position", "ParentBookId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_Document_ParentId",
                table: "SectionBase",
                column: "Document_ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_ParentId",
                table: "SectionBase",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_Title_Document_ParentId",
                table: "SectionBase",
                columns: new[] { "Title", "Document_ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionBase_Title_ParentId",
                table: "SectionBase",
                columns: new[] { "Title", "ParentId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerDMOPageDMO");

            migrationBuilder.DropTable(
                name: "DocumentTag");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "SectionBase");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "CategoryBase");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
