using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haliSahaRandevu.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToHaliSaha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Boylam",
                table: "HaliSahalar",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Enlem",
                table: "HaliSahalar",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boylam",
                table: "HaliSahalar");

            migrationBuilder.DropColumn(
                name: "Enlem",
                table: "HaliSahalar");
        }
    }
}
