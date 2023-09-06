using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lw.Infra.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class LinUser_Page : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pages_CreatedBy",
                table: "Pages",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Users_CreatedBy",
                table: "Pages",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Users_CreatedBy",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_CreatedBy",
                table: "Pages");
        }
    }
}
