using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddedMainEntities3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOperationType_Operations_TagsId",
                table: "DepartmentOperationType");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "DepartmentOperationType",
                newName: "OperationsId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentOperationType_TagsId",
                table: "DepartmentOperationType",
                newName: "IX_DepartmentOperationType_OperationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOperationType_Operations_OperationsId",
                table: "DepartmentOperationType",
                column: "OperationsId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOperationType_Operations_OperationsId",
                table: "DepartmentOperationType");

            migrationBuilder.RenameColumn(
                name: "OperationsId",
                table: "DepartmentOperationType",
                newName: "TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentOperationType_OperationsId",
                table: "DepartmentOperationType",
                newName: "IX_DepartmentOperationType_TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOperationType_Operations_TagsId",
                table: "DepartmentOperationType",
                column: "TagsId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
