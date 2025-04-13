using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class Borrowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bb5df38-c8ef-408b-b155-086e1c2b4c80");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e8aaf3e1-5c16-4d1c-9c1a-a8cf38e08102", "54b3d2aa-9c58-45db-9ddf-b1e5d43bc7b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8aaf3e1-5c16-4d1c-9c1a-a8cf38e08102");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b3d2aa-9c58-45db-9ddf-b1e5d43bc7b8");

            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookUser",
                columns: table => new
                {
                    BorrowedBooksId = table.Column<int>(type: "int", nullable: false),
                    BorrowersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUser", x => new { x.BorrowedBooksId, x.BorrowersId });
                    table.ForeignKey(
                        name: "FK_BookUser_AspNetUsers_BorrowersId",
                        column: x => x.BorrowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookUser_Books_BorrowedBooksId",
                        column: x => x.BorrowedBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_BorrowersId",
                table: "BookUser",
                column: "BorrowersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookUser");

            migrationBuilder.DropColumn(
                name: "Copies",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4bb5df38-c8ef-408b-b155-086e1c2b4c80", null, "User", "USER" },
                    { "e8aaf3e1-5c16-4d1c-9c1a-a8cf38e08102", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "54b3d2aa-9c58-45db-9ddf-b1e5d43bc7b8", 0, "ab54037e-9dc0-4b3a-b20e-74315f2ae61b", "admin@library.com", true, false, null, "ADMIN@LIBRARY.COM", "ADMIN@LIBRARY.COM", "AQAAAAIAAYagAAAAEP42Gqm56rzMxxaBnZRmrCL4OzsdKzj3I9Pe9AoE2tzDpHztqkT8Qi6CzxCzcF7yQA==", null, false, "b1f0c23d-6f50-4b77-b3e9-1e23f58e87b9", false, "admin@library.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e8aaf3e1-5c16-4d1c-9c1a-a8cf38e08102", "54b3d2aa-9c58-45db-9ddf-b1e5d43bc7b8" });
        }
    }
}
