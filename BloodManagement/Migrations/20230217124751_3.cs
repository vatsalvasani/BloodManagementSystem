using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodManagement.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_User_UserId",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Donor_User_UserId",
                table: "Donor");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "User1Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Donor",
                newName: "User1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Donor_UserId",
                table: "Donor",
                newName: "IX_Donor_User1Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Beneficiary",
                newName: "User1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_UserId",
                table: "Beneficiary",
                newName: "IX_Beneficiary_User1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_User_User1Id",
                table: "Beneficiary",
                column: "User1Id",
                principalTable: "User",
                principalColumn: "User1Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_User_User1Id",
                table: "Donor",
                column: "User1Id",
                principalTable: "User",
                principalColumn: "User1Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_User_User1Id",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Donor_User_User1Id",
                table: "Donor");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "Donor",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Donor_User1Id",
                table: "Donor",
                newName: "IX_Donor_UserId");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "Beneficiary",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_User1Id",
                table: "Beneficiary",
                newName: "IX_Beneficiary_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_User_UserId",
                table: "Beneficiary",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_User_UserId",
                table: "Donor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
