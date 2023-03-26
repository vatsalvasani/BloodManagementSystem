using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodManagement.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_User_User1Id",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Donor_User_User1Id",
                table: "Donor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "User1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User1",
                table: "User1",
                column: "User1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_User1_User1Id",
                table: "Beneficiary",
                column: "User1Id",
                principalTable: "User1",
                principalColumn: "User1Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_User1_User1Id",
                table: "Donor",
                column: "User1Id",
                principalTable: "User1",
                principalColumn: "User1Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_User1_User1Id",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Donor_User1_User1Id",
                table: "Donor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User1",
                table: "User1");

            migrationBuilder.RenameTable(
                name: "User1",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "User1Id");

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
    }
}
