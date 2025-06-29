using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunctionFood.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteAndStatusToInvoiceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Description", "Image", "Price", "ProductName", "Trending" },
                values: new object[] { "P001", "Vitamin, Chống oxy hóa", "Hỗ trợ tăng sức đề kháng và chống lão hóa", "/img/vitamin-c.jpg", 120000, "Viên uống Vitamin C 500mg", true });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Description", "Image", "Price", "ProductName" },
                values: new object[] { "P002", "Omega, Não bộ", "Hỗ trợ trí nhớ và tim mạch", "/image/omega3.jpg", 150000, "Omega-3 Fish Oil" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Description", "Image", "Price", "ProductName", "Trending" },
                values: new object[] { "P003", "Collagen, Làm đẹp", "Giúp da sáng mịn và đàn hồi tốt hơn", "/image/collagen.jpg", 220000, "Collagen Beauty Plus", true });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Description", "Image", "Price", "ProductName" },
                values: new object[] { "P004", "Tiêu hóa, Miễn dịch", "Cung cấp lợi khuẩn giúp hỗ trợ tiêu hóa và tăng cường miễn dịch", "/image/probiotic.jpg", 180000, "Probiotic Digestive Plus" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Description", "Image", "Price", "ProductName", "Trending" },
                values: new object[] { "P005", "Xương khớp, Canxi", "Giúp chắc xương, hỗ trợ hấp thu canxi hiệu quả", "/image/calcium.jpg", 135000, "Calcium + Vitamin D3", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P001");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P002");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P003");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P004");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "P005");
        }
    }
}
