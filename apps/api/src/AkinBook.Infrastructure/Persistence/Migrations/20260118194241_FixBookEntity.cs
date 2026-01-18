using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkinBook.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAd",
                table: "Books",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "PublicYear",
                table: "Books",
                newName: "PublishedYear");

            migrationBuilder.RenameColumn(
                name: "CreatedAd",
                table: "Books",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Books",
                newName: "UpdatedAd");

            migrationBuilder.RenameColumn(
                name: "PublishedYear",
                table: "Books",
                newName: "PublicYear");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Books",
                newName: "CreatedAd");
        }
    }
}
