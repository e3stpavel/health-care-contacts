using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtterlyComplete.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FacilityContactMechanism : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactMechanisms_Facilities_FacilityId",
                table: "ContactMechanisms");

            migrationBuilder.DropIndex(
                name: "IX_ContactMechanisms_FacilityId",
                table: "ContactMechanisms");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "ContactMechanisms");

            migrationBuilder.CreateTable(
                name: "ContactMechanismFacility",
                columns: table => new
                {
                    ContactMechanismsId = table.Column<int>(type: "INTEGER", nullable: false),
                    FacilitiesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMechanismFacility", x => new { x.ContactMechanismsId, x.FacilitiesId });
                    table.ForeignKey(
                        name: "FK_ContactMechanismFacility_ContactMechanisms_ContactMechanismsId",
                        column: x => x.ContactMechanismsId,
                        principalTable: "ContactMechanisms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactMechanismFacility_Facilities_FacilitiesId",
                        column: x => x.FacilitiesId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactMechanismFacility_FacilitiesId",
                table: "ContactMechanismFacility",
                column: "FacilitiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMechanismFacility");

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "ContactMechanisms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactMechanisms_FacilityId",
                table: "ContactMechanisms",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMechanisms_Facilities_FacilityId",
                table: "ContactMechanisms",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
