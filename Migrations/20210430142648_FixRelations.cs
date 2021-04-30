using Microsoft.EntityFrameworkCore.Migrations;

namespace BookKeeperBot.Migrations
{
    public partial class FixRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelves_BookshelfId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookshelves_Users_UserId",
                table: "Bookshelves");

            migrationBuilder.DropIndex(
                name: "IX_Users_SelectedBookshelfId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bookshelves",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookshelfId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SelectedBookshelfId",
                table: "Users",
                column: "SelectedBookshelfId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookshelves_BookshelfId",
                table: "Books",
                column: "BookshelfId",
                principalTable: "Bookshelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookshelves_Users_UserId",
                table: "Bookshelves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelves_BookshelfId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookshelves_Users_UserId",
                table: "Bookshelves");

            migrationBuilder.DropIndex(
                name: "IX_Users_SelectedBookshelfId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bookshelves",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BookshelfId",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SelectedBookshelfId",
                table: "Users",
                column: "SelectedBookshelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookshelves_BookshelfId",
                table: "Books",
                column: "BookshelfId",
                principalTable: "Bookshelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookshelves_Users_UserId",
                table: "Bookshelves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
