using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haliSahaRandevu.Migrations
{
    /// <inheritdoc />
    public partial class AddSahaFotolar_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SahaFotolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    HaliSahaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahaFotolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SahaFotolar_HaliSahalar_HaliSahaId",
                        column: x => x.HaliSahaId,
                        principalTable: "HaliSahalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SahaFotolar_HaliSahaId",
                table: "SahaFotolar",
                column: "HaliSahaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SahaFotolar");
        }
    }
}
