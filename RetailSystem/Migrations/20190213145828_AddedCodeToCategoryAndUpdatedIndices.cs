using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class AddedCodeToCategoryAndUpdatedIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transfers_SourceLocationId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "Transfer_Number",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Supplies_LocationId",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "Supply_ReferenceNumber",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_BusinessId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Supplier_Name",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Supplier_Number",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Sales_LocationId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "Sale_ReferenceNumber",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_LocationId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "PurchaseOrder_Number",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_BusinessId",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "Manufacturer_Name",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Locations_BusinessId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "Location_Name",
                table: "Locations");

            migrationBuilder.DropUniqueConstraint(
                name: "Item_Code",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "Item_Description",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BusinessId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "Category_Name",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Categories",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "Category_Item_Code",
                table: "Items",
                columns: new[] { "CategoryId", "Code" });

            migrationBuilder.CreateIndex(
                name: "Location_Transfer_Number",
                table: "Transfers",
                columns: new[] { "SourceLocationId", "DestinationLocationId", "TransferNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Location_Supply_ReferenceNumber",
                table: "Supplies",
                columns: new[] { "LocationId", "ReferenceNumber" },
                unique: true,
                filter: "[ReferenceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Business_Supplier_Name",
                table: "Suppliers",
                columns: new[] { "BusinessId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Business_Supplier_Number",
                table: "Suppliers",
                columns: new[] { "BusinessId", "SupplierNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Location_Sale_ReferenceNumber",
                table: "Sales",
                columns: new[] { "LocationId", "ReferenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Business_Order_Number",
                table: "Purchases",
                columns: new[] { "LocationId", "OrderNumber" },
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Business_Manufacturer_Name",
                table: "Manufacturers",
                columns: new[] { "BusinessId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Business_Location_Name",
                table: "Locations",
                columns: new[] { "BusinessId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Category_Item_Description",
                table: "Items",
                columns: new[] { "CategoryId", "Description" });

            migrationBuilder.CreateIndex(
                name: "Business_Category_Code",
                table: "Categories",
                columns: new[] { "BusinessId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Business_Category_Name",
                table: "Categories",
                columns: new[] { "BusinessId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Location_Transfer_Number",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "Location_Supply_ReferenceNumber",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "Business_Supplier_Name",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Business_Supplier_Number",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Location_Sale_ReferenceNumber",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "Business_Order_Number",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "Business_Manufacturer_Name",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "Business_Location_Name",
                table: "Locations");

            migrationBuilder.DropUniqueConstraint(
                name: "Category_Item_Code",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "Category_Item_Description",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "Business_Category_Code",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "Business_Category_Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddUniqueConstraint(
                name: "Item_Code",
                table: "Items",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SourceLocationId",
                table: "Transfers",
                column: "SourceLocationId");

            migrationBuilder.CreateIndex(
                name: "Transfer_Number",
                table: "Transfers",
                column: "TransferNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_LocationId",
                table: "Supplies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "Supply_ReferenceNumber",
                table: "Supplies",
                column: "ReferenceNumber",
                unique: true,
                filter: "[ReferenceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BusinessId",
                table: "Suppliers",
                column: "BusinessId");

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
                name: "IX_Sales_LocationId",
                table: "Sales",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "Sale_ReferenceNumber",
                table: "Sales",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_LocationId",
                table: "Purchases",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "PurchaseOrder_Number",
                table: "Purchases",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_BusinessId",
                table: "Manufacturers",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "Manufacturer_Name",
                table: "Manufacturers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_BusinessId",
                table: "Locations",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "Location_Name",
                table: "Locations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "Item_Description",
                table: "Items",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BusinessId",
                table: "Categories",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "Category_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }
    }
}
