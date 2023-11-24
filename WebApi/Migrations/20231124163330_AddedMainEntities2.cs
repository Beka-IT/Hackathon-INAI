using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddedMainEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationTypeId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "DepartmentOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OperationId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentOperations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentOperations_Operations_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentOperationType",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentOperationType", x => new { x.DepartmentId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DepartmentOperationType_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentOperationType_Operations_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOperations_DepartmentId",
                table: "DepartmentOperations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOperations_OperationTypeId",
                table: "DepartmentOperations",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOperationType_TagsId",
                table: "DepartmentOperationType",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentOperations");

            migrationBuilder.DropTable(
                name: "DepartmentOperationType");

            migrationBuilder.AddColumn<int>(
                name: "OperationTypeId",
                table: "Departments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
