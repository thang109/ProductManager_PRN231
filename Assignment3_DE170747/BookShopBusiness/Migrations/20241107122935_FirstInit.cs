using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookShopBusiness.Migrations
{
    /// <inheritdoc />
    public partial class FirstInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    ShippingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    DateBooking = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateShip = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationShip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserOrderId = table.Column<int>(type: "int", nullable: true),
                    UserApproveId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooksBookId = table.Column<int>(type: "int", nullable: true),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.ShippingId);
                    table.ForeignKey(
                        name: "FK_Shippings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId");
                    table.ForeignKey(
                        name: "FK_Shippings_Users_UserApproveId",
                        column: x => x.UserApproveId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_Users_UserOrderId",
                        column: x => x.UserOrderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Non-fiction" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "password123", "Test1" },
                    { 2, "password456", "Test2" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "BookName", "CategoryId", "Price" },
                values: new object[,]
                {
                    { 1, "The Great Gatsby", 1, 10 },
                    { 2, "Sapiens", 2, 12 }
                });

            migrationBuilder.InsertData(
                table: "Shippings",
                columns: new[] { "ShippingId", "BookId", "BooksBookId", "DateBooking", "DateShip", "LocationShip", "Status", "UserApproveId", "UserOrderId", "UsersUserId" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2024, 11, 7, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6748), new DateTime(2024, 11, 9, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6789), "New York", "isApprove", 2, 1, null },
                    { 2, 2, null, new DateTime(2024, 11, 7, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6814), new DateTime(2024, 11, 10, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6815), "California", "isApprove", 1, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_BookId",
                table: "Shippings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_BooksBookId",
                table: "Shippings",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_UserApproveId",
                table: "Shippings",
                column: "UserApproveId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_UserOrderId",
                table: "Shippings",
                column: "UserOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_UsersUserId",
                table: "Shippings",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
