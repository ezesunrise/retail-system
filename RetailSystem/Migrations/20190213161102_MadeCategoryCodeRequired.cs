using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class MadeCategoryCodeRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Business_Category_Code",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Categories",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "Business_Category_Code",
                table: "Categories",
                columns: new[] { "BusinessId", "Code" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Business_Category_Code",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Categories",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3);

            migrationBuilder.CreateIndex(
                name: "Business_Category_Code",
                table: "Categories",
                columns: new[] { "BusinessId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");
        }
    }
}
