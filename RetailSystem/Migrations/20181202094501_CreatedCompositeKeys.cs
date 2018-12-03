using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class CreatedCompositeKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItems_TransferId",
                table: "TransferItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleItems",
                table: "SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportItems",
                table: "ReportItems");

            migrationBuilder.DropIndex(
                name: "IX_ReportItems_ReportGroupId",
                table: "ReportItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_PurchaseOrderId",
                table: "PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationItems",
                table: "LocationItems");

            migrationBuilder.DropIndex(
                name: "IX_LocationItems_LocationId",
                table: "LocationItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItem",
                table: "InvoiceItem");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReportItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LocationItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InvoiceItem");

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
                name: "Order_Item_Id",
                table: "OrderItems",
                columns: new[] { "OrderId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "Location_Item_Id",
                table: "LocationItems",
                columns: new[] { "LocationId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "Invoice_Item_Id",
                table: "InvoiceItem",
                columns: new[] { "InvoiceId", "ItemId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Order_Item_Id",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "Location_Item_Id",
                table: "LocationItems");

            migrationBuilder.DropPrimaryKey(
                name: "Invoice_Item_Id",
                table: "InvoiceItem");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TransferItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SaleItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReportItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PurchaseItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LocationItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InvoiceItem",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleItems",
                table: "SaleItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportItems",
                table: "ReportItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationItems",
                table: "LocationItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItem",
                table: "InvoiceItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_TransferId",
                table: "TransferItems",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportItems_ReportGroupId",
                table: "ReportItems",
                column: "ReportGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PurchaseOrderId",
                table: "PurchaseItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationItems_LocationId",
                table: "LocationItems",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");
        }
    }
}
