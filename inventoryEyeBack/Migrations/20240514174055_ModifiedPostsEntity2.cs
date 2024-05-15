using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventoryEyeBack.Migrations
{
    public partial class ModifiedPostsEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category",
                table: "posts",
                newName: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "posts",
                newName: "category");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
