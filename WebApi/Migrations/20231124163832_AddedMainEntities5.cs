using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddedMainEntities5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOperationType_Departments_DepartmentId",
                table: "DepartmentOperationType");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "DepartmentOperationType",
                newName: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOperationType_Departments_DepartmentsId",
                table: "DepartmentOperationType",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOperationType_Departments_DepartmentsId",
                table: "DepartmentOperationType");

            migrationBuilder.RenameColumn(
                name: "DepartmentsId",
                table: "DepartmentOperationType",
                newName: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOperationType_Departments_DepartmentId",
                table: "DepartmentOperationType",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
