using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunctionFood.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiverInfoToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P002",
                column: "Image",
                value: "/img/omega3.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P003",
                column: "Image",
                value: "/img/collagen.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P004",
                column: "Image",
                value: "/img/probiotic.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P005",
                column: "Image",
                value: "/img/calcium.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P002",
                column: "Image",
                value: "/image/omega3.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P003",
                column: "Image",
                value: "/image/collagen.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P004",
                column: "Image",
                value: "/image/probiotic.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P005",
                column: "Image",
                value: "/image/calcium.jpg");
        }
    }
}
