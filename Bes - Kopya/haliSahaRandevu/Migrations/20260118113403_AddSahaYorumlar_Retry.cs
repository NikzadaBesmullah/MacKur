using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haliSahaRandevu.Migrations
{
    /// <inheritdoc />
    public partial class AddSahaYorumlar_Retry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SahaYorumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Yorum = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Puan = table.Column<int>(type: "INTEGER", nullable: false),
                    Tarih = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HaliSahaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UyeId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahaYorumlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SahaYorumlar_AspNetUsers_UyeId",
                        column: x => x.UyeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SahaYorumlar_HaliSahalar_HaliSahaId",
                        column: x => x.HaliSahaId,
                        principalTable: "HaliSahalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SahaYorumlar_HaliSahaId",
                table: "SahaYorumlar",
                column: "HaliSahaId");

            migrationBuilder.CreateIndex(
                name: "IX_SahaYorumlar_UyeId",
                table: "SahaYorumlar",
                column: "UyeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SahaYorumlar");
        }
    }
}
