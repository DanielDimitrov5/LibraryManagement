using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddGenresTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Books",
                newName: "GenreId");

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "NonFiction" },
                    { 3, "Mystery" },
                    { 4, "Thriller" },
                    { 5, "Fantasy" },
                    { 6, "ScienceFiction" },
                    { 7, "Romance" },
                    { 8, "Horror" },
                    { 9, "Historical" },
                    { 10, "Adventure" },
                    { 11, "Biography" },
                    { 12, "SelfHelp" },
                    { 13, "Poetry" },
                    { 14, "GraphicNovel" },
                    { 15, "Children" },
                    { 16, "YoungAdult" },
                    { 17, "Dystopian" },
                    { 18, "Classic" },
                    { 19, "Spirituality" },
                    { 20, "Science" },
                    { 21, "Philosophy" },
                    { 22, "Business" },
                    { 23, "Travel" },
                    { 24, "Cooking" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genre_GenreId",
                table: "Books",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genre_GenreId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Books_GenreId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Books",
                newName: "Genre");
        }
    }
}
