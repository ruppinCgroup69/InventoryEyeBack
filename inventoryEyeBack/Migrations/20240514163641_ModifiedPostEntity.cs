using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventoryEyeBack.Migrations
{
    public partial class ModifiedPostEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "posts");
        }
    }
}
