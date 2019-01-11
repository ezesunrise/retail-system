using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class MadeIndicesUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Transfer_Number",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "Supply_ReferenceNumber",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "Supplier_Name",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Supplier_Number",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Sale_ReferenceNumber",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "Receipt_Number",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "PurchaseOrder_Number",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "Manufacturer_Name",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "Location_Name",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "Invoice_Number",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "Category_Name",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "Transfer_Number",
                table: "Transfers",
                column: "TransferNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Supply_ReferenceNumber",
                table: "Supplies",
                column: "ReferenceNumber",
                unique: true,
                filter: "[ReferenceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Supplier_Name",
                table: "Suppliers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Supplier_Number",
                table: "Suppliers",
                column: "SupplierNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "SubCategory_Name_CategoryId",
                table: "SubCategories",
                columns: new[] { "Name", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Sale_ReferenceNumber",
                table: "Sales",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Receipt_Number",
                table: "Receipts",
                column: "ReceiptNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PurchaseOrder_Number",
                table: "Purchases",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Manufacturer_Name",
                table: "Manufacturers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Location_Name",
                table: "Locations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Invoice_Number",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Category_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Transfer_Number",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "Supply_ReferenceNumber",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "Supplier_Name",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Supplier_Number",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "SubCategory_Name_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "Sale_ReferenceNumber",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "Receipt_Number",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "PurchaseOrder_Number",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "Manufacturer_Name",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "Location_Name",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "Invoice_Number",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "Category_Name",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "Transfer_Number",
                table: "Transfers",
                column: "TransferNumber");

            migrationBuilder.CreateIndex(
                name: "Supply_ReferenceNumber",
                table: "Supplies",
                column: "ReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "Supplier_Name",
                table: "Suppliers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "Supplier_Number",
                table: "Suppliers",
                column: "SupplierNumber");

            migrationBuilder.CreateIndex(
                name: "Sale_ReferenceNumber",
                table: "Sales",
                column: "ReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "Receipt_Number",
                table: "Receipts",
                column: "ReceiptNumber");

            migrationBuilder.CreateIndex(
                name: "PurchaseOrder_Number",
                table: "Purchases",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "Manufacturer_Name",
                table: "Manufacturers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "Location_Name",
                table: "Locations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "Invoice_Number",
                table: "Invoices",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "Category_Name",
                table: "Categories",
                column: "Name");
        }
    }
}
