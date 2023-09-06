using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lw.Infra.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class WebsiteSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_Websites_WebsiteId",
                table: "Scripts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scripts",
                table: "Scripts");

            migrationBuilder.RenameTable(
                name: "Scripts",
                newName: "WebsiteSettings");

            migrationBuilder.RenameIndex(
                name: "IX_Scripts_WebsiteId",
                table: "WebsiteSettings",
                newName: "IX_WebsiteSettings_WebsiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteSettings",
                table: "WebsiteSettings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WebsiteSettings_Websites_WebsiteId",
                table: "WebsiteSettings",
                column: "WebsiteId",
                principalTable: "Websites",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteSettings_Websites_WebsiteId",
                table: "WebsiteSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteSettings",
                table: "WebsiteSettings");

            migrationBuilder.RenameTable(
                name: "WebsiteSettings",
                newName: "Scripts");

            migrationBuilder.RenameIndex(
                name: "IX_WebsiteSettings_WebsiteId",
                table: "Scripts",
                newName: "IX_Scripts_WebsiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scripts",
                table: "Scripts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_Websites_WebsiteId",
                table: "Scripts",
                column: "WebsiteId",
                principalTable: "Websites",
                principalColumn: "Id");
        }
    }
}
