using Microsoft.EntityFrameworkCore.Migrations;

namespace BookKeeperBot.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    PreviousCommand = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedBookshelfId = table.Column<int>(type: "INTEGER", nullable: true),
                    SelectedBookId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookshelves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshelves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookshelves_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ImageId = table.Column<string>(type: "TEXT", nullable: true),
                    BookshelfId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Bookshelves_BookshelfId",
                        column: x => x.BookshelfId,
                        principalTable: "Bookshelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookshelfId",
                table: "Books",
                column: "BookshelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookshelves_UserId",
                table: "Bookshelves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SelectedBookId",
                table: "Users",
                column: "SelectedBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SelectedBookshelfId",
                table: "Users",
                column: "SelectedBookshelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Books_SelectedBookId",
                table: "Users",
                column: "SelectedBookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Bookshelves_SelectedBookshelfId",
                table: "Users",
                column: "SelectedBookshelfId",
                principalTable: "Bookshelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelves_BookshelfId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Bookshelves_SelectedBookshelfId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Bookshelves");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
