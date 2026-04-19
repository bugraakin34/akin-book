using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkinBook.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalizedBookFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "TitleTr");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "DescriptionTr");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "TitleTr",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DescriptionTr",
                table: "Books",
                newName: "Description");
        }
    }
}
