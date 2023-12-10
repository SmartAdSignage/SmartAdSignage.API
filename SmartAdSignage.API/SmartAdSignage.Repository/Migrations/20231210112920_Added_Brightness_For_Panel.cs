using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAdSignage.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Added_Brightness_For_Panel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Brightness",
                table: "Panels",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brightness",
                table: "Panels");
        }
    }
}
