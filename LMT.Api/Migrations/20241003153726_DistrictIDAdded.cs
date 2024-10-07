using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DistrictIDAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "District_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District_Id",
                table: "AspNetUsers");
        }
    }
}
