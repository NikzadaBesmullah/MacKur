using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haliSahaRandevu.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Favoriler_AspNetUsers_UyeId",
                table: "Favoriler");

            migrationBuilder.DropForeignKey(
                name: "FK_Favoriler_HaliSahalar_HaliSahaId",
                table: "Favoriler");

            migrationBuilder.DropForeignKey(
                name: "FK_HaliSahalar_AspNetUsers_SahibiId",
                table: "HaliSahalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_SahaYorumlar_AspNetUsers_UyeId",
                table: "SahaYorumlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favoriler",
                table: "Favoriler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Favoriler",
                newName: "FavoriSahalar");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "KullaniciTokenlari");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Kullanicilar");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "KullaniciRolleri");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "KullaniciGirisleri");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "KullaniciYetkileri");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Roller");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "RolYetkileri");

            migrationBuilder.RenameIndex(
                name: "IX_Favoriler_UyeId",
                table: "FavoriSahalar",
                newName: "IX_FavoriSahalar_UyeId");

            migrationBuilder.RenameIndex(
                name: "IX_Favoriler_HaliSahaId",
                table: "FavoriSahalar",
                newName: "IX_FavoriSahalar_HaliSahaId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "KullaniciRolleri",
                newName: "IX_KullaniciRolleri_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "KullaniciGirisleri",
                newName: "IX_KullaniciGirisleri_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "KullaniciYetkileri",
                newName: "IX_KullaniciYetkileri_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "RolYetkileri",
                newName: "IX_RolYetkileri_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriSahalar",
                table: "FavoriSahalar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciTokenlari",
                table: "KullaniciTokenlari",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciRolleri",
                table: "KullaniciRolleri",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciGirisleri",
                table: "KullaniciGirisleri",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciYetkileri",
                table: "KullaniciYetkileri",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roller",
                table: "Roller",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolYetkileri",
                table: "RolYetkileri",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriSahalar_HaliSahalar_HaliSahaId",
                table: "FavoriSahalar",
                column: "HaliSahaId",
                principalTable: "HaliSahalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriSahalar_Kullanicilar_UyeId",
                table: "FavoriSahalar",
                column: "UyeId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HaliSahalar_Kullanicilar_SahibiId",
                table: "HaliSahalar",
                column: "SahibiId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciGirisleri_Kullanicilar_UserId",
                table: "KullaniciGirisleri",
                column: "UserId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciRolleri_Kullanicilar_UserId",
                table: "KullaniciRolleri",
                column: "UserId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciRolleri_Roller_RoleId",
                table: "KullaniciRolleri",
                column: "RoleId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciTokenlari_Kullanicilar_UserId",
                table: "KullaniciTokenlari",
                column: "UserId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciYetkileri_Kullanicilar_UserId",
                table: "KullaniciYetkileri",
                column: "UserId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolYetkileri_Roller_RoleId",
                table: "RolYetkileri",
                column: "RoleId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SahaYorumlar_Kullanicilar_UyeId",
                table: "SahaYorumlar",
                column: "UyeId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriSahalar_HaliSahalar_HaliSahaId",
                table: "FavoriSahalar");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriSahalar_Kullanicilar_UyeId",
                table: "FavoriSahalar");

            migrationBuilder.DropForeignKey(
                name: "FK_HaliSahalar_Kullanicilar_SahibiId",
                table: "HaliSahalar");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciGirisleri_Kullanicilar_UserId",
                table: "KullaniciGirisleri");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciRolleri_Kullanicilar_UserId",
                table: "KullaniciRolleri");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciRolleri_Roller_RoleId",
                table: "KullaniciRolleri");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciTokenlari_Kullanicilar_UserId",
                table: "KullaniciTokenlari");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciYetkileri_Kullanicilar_UserId",
                table: "KullaniciYetkileri");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_RolYetkileri_Roller_RoleId",
                table: "RolYetkileri");

            migrationBuilder.DropForeignKey(
                name: "FK_SahaYorumlar_Kullanicilar_UyeId",
                table: "SahaYorumlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolYetkileri",
                table: "RolYetkileri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roller",
                table: "Roller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciYetkileri",
                table: "KullaniciYetkileri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciTokenlari",
                table: "KullaniciTokenlari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciRolleri",
                table: "KullaniciRolleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciGirisleri",
                table: "KullaniciGirisleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriSahalar",
                table: "FavoriSahalar");

            migrationBuilder.RenameTable(
                name: "RolYetkileri",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Roller",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "KullaniciYetkileri",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "KullaniciTokenlari",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "KullaniciRolleri",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "Kullanicilar",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "KullaniciGirisleri",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "FavoriSahalar",
                newName: "Favoriler");

            migrationBuilder.RenameIndex(
                name: "IX_RolYetkileri_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_KullaniciYetkileri_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KullaniciRolleri_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_KullaniciGirisleri_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriSahalar_UyeId",
                table: "Favoriler",
                newName: "IX_Favoriler_UyeId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriSahalar_HaliSahaId",
                table: "Favoriler",
                newName: "IX_Favoriler_HaliSahaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favoriler",
                table: "Favoriler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favoriler_AspNetUsers_UyeId",
                table: "Favoriler",
                column: "UyeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favoriler_HaliSahalar_HaliSahaId",
                table: "Favoriler",
                column: "HaliSahaId",
                principalTable: "HaliSahalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HaliSahalar_AspNetUsers_SahibiId",
                table: "HaliSahalar",
                column: "SahibiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_KullaniciId",
                table: "Randevular",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SahaYorumlar_AspNetUsers_UyeId",
                table: "SahaYorumlar",
                column: "UyeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
