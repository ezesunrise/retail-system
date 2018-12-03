using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class AddedUniqueIndicesAndProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "Transfer_Item_Id",
                table: "TransferItems");

            migrationBuilder.DropPrimaryKey(
                name: "Sale_Item_Id",
                table: "SaleItems");

            migrationBuilder.DropPrimaryKey(
                name: "Report_Item_Id",
                table: "ReportItems");

            migrationBuilder.DropPrimaryKey(
                name: "Purchase_Item_Id",
                table: "PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "Location_Item_Id",
                table: "LocationItems");

            migrationBuilder.DropPrimaryKey(
                name: "Invoice_Item_Id",
                table: "InvoiceItem");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "Transfers",
                newName: "TransferNumber");

            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "Receipts",
                newName: "ReceiptNumber");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "LocationItems",
                newName: "FaultQuantity");

            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "Invoices",
                newName: "InvoiceNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Transfers",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransferNumber",
                table: "Transfers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "TransferItems",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCode",
                table: "Suppliers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Sales",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CashPaid",
                table: "Receipts",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Receipts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Purchases",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedReceiptDate",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Purchases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                table: "PurchaseItems",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<int>(
                name: "FaultQuantity",
                table: "PurchaseItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityDelivered",
                table: "PurchaseItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Locations",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<byte>(
                name: "DiscountQuantity",
                table: "LocationItems",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LocationItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Businesses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "TransferItem_Id",
                table: "TransferItems",
                columns: new[] { "TransferId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "SaleItem_Id",
                table: "SaleItems",
                columns: new[] { "SaleId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "ReportItem_Id",
                table: "ReportItems",
                columns: new[] { "ReportGroupId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PurchaseOrderItem_Id",
                table: "PurchaseItems",
                columns: new[] { "PurchaseOrderId", "ItemId" });

            migrationBuilder.AddUniqueConstraint(
                name: "Location_Code",
                table: "Locations",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "LocationItem_Id",
                table: "LocationItems",
                columns: new[] { "LocationId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "InvoiceItem_Id",
                table: "InvoiceItem",
                columns: new[] { "InvoiceId", "ItemId" });

            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    UpdaterId = table.Column<string>(nullable: true),
                    ReferenceNumber = table.Column<string>(nullable: true),
                    Note = table.Column<string>(maxLength: 1024, nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    ExpectedDeliveryDate = table.Column<DateTime>(nullable: true),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplies_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplies_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplies_AspNetUsers_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplyItems",
                columns: table => new
                {
                    Note = table.Column<string>(maxLength: 512, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    QuantityDelivered = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    SupplyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SupplyItem_Id", x => new { x.SupplyId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_SupplyItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplyItems_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "Unit_Name",
                table: "Units",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "Transfer_Number",
                table: "Transfers",
                column: "TransferNumber");

            migrationBuilder.CreateIndex(
                name: "Supplier_Name",
                table: "Suppliers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "Supplier_Code",
                table: "Suppliers",
                column: "SupplierCode");

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
                name: "Item_Description",
                table: "Items",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "Invoice_Number",
                table: "Invoices",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "Category_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_CreatorId",
                table: "Supplies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_LocationId",
                table: "Supplies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "Supply_ReferenceNumber",
                table: "Supplies",
                column: "ReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_UpdaterId",
                table: "Supplies",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyItems_ItemId",
                table: "SupplyItems",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplyItems");

            migrationBuilder.DropTable(
                name: "Supplies");

            migrationBuilder.DropIndex(
                name: "Unit_Name",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "Transfer_Number",
                table: "Transfers");

            migrationBuilder.DropPrimaryKey(
                name: "TransferItem_Id",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "Supplier_Name",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Supplier_Code",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "Sale_ReferenceNumber",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "SaleItem_Id",
                table: "SaleItems");

            migrationBuilder.DropPrimaryKey(
                name: "ReportItem_Id",
                table: "ReportItems");

            migrationBuilder.DropIndex(
                name: "Receipt_Number",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "PurchaseOrder_Number",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PurchaseOrderItem_Id",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "Manufacturer_Name",
                table: "Manufacturers");

            migrationBuilder.DropUniqueConstraint(
                name: "Location_Code",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "Location_Name",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "LocationItem_Id",
                table: "LocationItems");

            migrationBuilder.DropIndex(
                name: "Item_Description",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "Invoice_Number",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "InvoiceItem_Id",
                table: "InvoiceItem");

            migrationBuilder.DropIndex(
                name: "Category_Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ExpectedReceiptDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "FaultQuantity",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "QuantityDelivered",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LocationItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Businesses");

            migrationBuilder.RenameColumn(
                name: "TransferNumber",
                table: "Transfers",
                newName: "ReferenceNumber");

            migrationBuilder.RenameColumn(
                name: "ReceiptNumber",
                table: "Receipts",
                newName: "ReferenceNumber");

            migrationBuilder.RenameColumn(
                name: "FaultQuantity",
                table: "LocationItems",
                newName: "Condition");

            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "Invoices",
                newName: "ReferenceNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Transfers",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "Transfers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transfers",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "TransferItems",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TransferItems",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCode",
                table: "Suppliers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Sales",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sales",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Sales",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "SaleItems",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CashPaid",
                table: "Receipts",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "Receipts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Purchases",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Purchases",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                table: "PurchaseItems",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PurchaseItems",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<byte>(
                name: "DiscountQuantity",
                table: "LocationItems",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Invoices",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "Transfer_Item_Id",
                table: "TransferItems",
                columns: new[] { "TransferId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "Sale_Item_Id",
                table: "SaleItems",
                columns: new[] { "SaleId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "Report_Item_Id",
                table: "ReportItems",
                columns: new[] { "ReportGroupId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "Purchase_Item_Id",
                table: "PurchaseItems",
                columns: new[] { "PurchaseOrderId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "Location_Item_Id",
                table: "LocationItems",
                columns: new[] { "LocationId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "Invoice_Item_Id",
                table: "InvoiceItem",
                columns: new[] { "InvoiceId", "ItemId" });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExpectedReceiptDate = table.Column<DateTime>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    PurchaseOrderId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdaterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Purchases_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 512, nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_Item_Id", x => new { x.OrderId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatorId",
                table: "Orders",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationId",
                table: "Orders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PurchaseOrderId",
                table: "Orders",
                column: "PurchaseOrderId",
                unique: true,
                filter: "[PurchaseOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UpdaterId",
                table: "Orders",
                column: "UpdaterId");
        }
    }
}
