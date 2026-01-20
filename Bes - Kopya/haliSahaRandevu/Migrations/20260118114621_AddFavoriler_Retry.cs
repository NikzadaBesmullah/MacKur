using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haliSahaRandevu.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriler_Retry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UyeId = table.Column<string>(type: "TEXT", nullable: false),
                    HaliSahaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoriler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoriler_AspNetUsers_UyeId",
                        column: x => x.UyeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoriler_HaliSahalar_HaliSahaId",
                        column: x => x.HaliSahaId,
                        principalTable: "HaliSahalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favoriler_HaliSahaId",
                table: "Favoriler",
                column: "HaliSahaId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoriler_UyeId",
                table: "Favoriler",
                column: "UyeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoriler");
        }
    }
}
