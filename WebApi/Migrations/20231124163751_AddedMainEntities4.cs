using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddedMainEntities4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOperations_Operations_OperationTypeId",
                table: "DepartmentOperations");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentOperations_OperationTypeId",
                table: "DepartmentOperations");

            migrationBuilder.DropColumn(
                name: "OperationTypeId",
                table: "DepartmentOperations");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOperations_OperationId",
                table: "DepartmentOperations",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOperations_Operations_OperationId",
                table: "DepartmentOperations",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOperations_Operations_OperationId",
                table: "DepartmentOperations");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentOperations_OperationId",
                table: "DepartmentOperations");

            migrationBuilder.AddColumn<int>(
                name: "OperationTypeId",
                table: "DepartmentOperations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOperations_OperationTypeId",
                table: "DepartmentOperations",
                column: "OperationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOperations_Operations_OperationTypeId",
                table: "DepartmentOperations",
                column: "OperationTypeId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
