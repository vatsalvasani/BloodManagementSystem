using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodManagement.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Review1_User1Id",
                table: "Review1",
                column: "User1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review1_User1_User1Id",
                table: "Review1",
                column: "User1Id",
                principalTable: "User1",
                principalColumn: "User1Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review1_User1_User1Id",
                table: "Review1");

            migrationBuilder.DropIndex(
                name: "IX_Review1_User1Id",
                table: "Review1");
        }
    }
}
