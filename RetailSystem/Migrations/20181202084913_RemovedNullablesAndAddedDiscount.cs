using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class RemovedNullablesAndAddedDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SaleItems");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "SaleItems",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "SaleItems",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PurchaseItems",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchaseItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitCost",
                table: "PurchaseItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "LocationItems",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "LocationItems",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "OptimumQuantity",
                table: "LocationItems",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "LowQuantity",
                table: "LocationItems",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<byte>(
                name: "DiscountQuantity",
                table: "LocationItems",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "PercentDiscount",
                table: "LocationItems",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Items",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Tax",
                table: "Items",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "DiscountQuantity",
                table: "LocationItems");

            migrationBuilder.DropColumn(
                name: "PercentDiscount",
                table: "LocationItems");

            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                table: "SaleItems",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "SaleItems",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SaleItems",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "LocationItems",
                type: "decimal(8,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                table: "LocationItems",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "OptimumQuantity",
                table: "LocationItems",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "LowQuantity",
                table: "LocationItems",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Items",
                type: "decimal(8,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<byte>(
                name: "Tax",
                table: "Items",
                nullable: true,
                oldClrType: typeof(byte));
        }
    }
}
