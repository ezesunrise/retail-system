using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class ChangedPropertyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Supplier_Code",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SupplierCode",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Suppliers",
                newName: "PhoneNumber1");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Suppliers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "AlternatePhoneNumber",
                table: "Suppliers",
                newName: "PhoneNumber2");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Locations",
                newName: "PhoneNumber1");

            migrationBuilder.RenameColumn(
                name: "AlternatePhoneNumber",
                table: "Locations",
                newName: "PhoneNumber2");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "PhoneNumber1");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Customers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "AlternatePhoneNumber",
                table: "Customers",
                newName: "PhoneNumber2");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierNumber",
                table: "Suppliers",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Manufacturers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "Supplier_Number",
                table: "Suppliers",
                column: "SupplierNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Supplier_Number",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SupplierNumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber1",
                table: "Suppliers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Suppliers",
                newName: "EmailAddress");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber2",
                table: "Suppliers",
                newName: "AlternatePhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber1",
                table: "Locations",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber2",
                table: "Locations",
                newName: "AlternatePhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber1",
                table: "Customers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customers",
                newName: "EmailAddress");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber2",
                table: "Customers",
                newName: "AlternatePhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "SupplierCode",
                table: "Suppliers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "Supplier_Code",
                table: "Suppliers",
                column: "SupplierCode");
        }
    }
}
