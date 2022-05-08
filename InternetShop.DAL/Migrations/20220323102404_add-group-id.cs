using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetShop.DAL.Migrations
{
    public partial class addgroupid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Groups_GroupId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Groups_GroupId",
                table: "Products",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Groups_GroupId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Groups_GroupId",
                table: "Products",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
