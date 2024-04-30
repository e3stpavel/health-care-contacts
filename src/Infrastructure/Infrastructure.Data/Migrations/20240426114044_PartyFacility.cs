using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UtterlyComplete.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PartyFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Facilities_FacilityId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_FacilityId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "Parties");

            migrationBuilder.CreateTable(
                name: "FacilityRoleType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityRoleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyFacility",
                columns: table => new
                {
                    PartyId = table.Column<int>(type: "INTEGER", nullable: false),
                    FacilityId = table.Column<int>(type: "INTEGER", nullable: false),
                    FacilityRoleTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyFacility", x => new { x.FacilityId, x.PartyId });
                    table.ForeignKey(
                        name: "FK_PartyFacility_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyFacility_FacilityRoleType_FacilityRoleTypeId",
                        column: x => x.FacilityRoleTypeId,
                        principalTable: "FacilityRoleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyFacility_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FacilityRoleType",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 0, "OWNED" },
                    { 1, "LEASED" },
                    { 2, "BOOKED" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyFacility_FacilityRoleTypeId",
                table: "PartyFacility",
                column: "FacilityRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyFacility_PartyId",
                table: "PartyFacility",
                column: "PartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyFacility");

            migrationBuilder.DropTable(
                name: "FacilityRoleType");

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "Parties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_FacilityId",
                table: "Parties",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Facilities_FacilityId",
                table: "Parties",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
