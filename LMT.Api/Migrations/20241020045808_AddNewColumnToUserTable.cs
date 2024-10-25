using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "AspNetUsers",
                type: "varchar(512)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "AspNetUsers");
        }
    }
}
