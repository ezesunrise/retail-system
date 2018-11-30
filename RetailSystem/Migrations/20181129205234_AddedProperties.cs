using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSystem.Migrations
{
    public partial class AddedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_CreatorId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Locations_LocationId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_UpdaterId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Businesses_BusinessId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_UpdaterId",
                table: "Invoices",
                newName: "IX_Invoices_UpdaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_LocationId",
                table: "Invoices",
                newName: "IX_Invoices_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_CreatorId",
                table: "Invoices",
                newName: "IX_Invoices_CreatorId");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Units",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenceNumber = table.Column<string>(nullable: false),
                    SaleId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    CashPaid = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SaleId",
                table: "Receipts",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoices_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatorId",
                table: "Invoices",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Locations_LocationId",
                table: "Invoices",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_UpdaterId",
                table: "Invoices",
                column: "UpdaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Businesses_BusinessId",
                table: "Units",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoices_InvoiceId",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatorId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Locations_LocationId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_UpdaterId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Businesses_BusinessId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_UpdaterId",
                table: "Invoice",
                newName: "IX_Invoice_UpdaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_LocationId",
                table: "Invoice",
                newName: "IX_Invoice_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_CreatorId",
                table: "Invoice",
                newName: "IX_Invoice_CreatorId");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Units",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_CreatorId",
                table: "Invoice",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Locations_LocationId",
                table: "Invoice",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_UpdaterId",
                table: "Invoice",
                column: "UpdaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Businesses_BusinessId",
                table: "Units",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
