using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeVoyage.Migrations
{
    /// <inheritdoc />
    public partial class codevoyage_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "BlogPosts");
        }
    }
}
