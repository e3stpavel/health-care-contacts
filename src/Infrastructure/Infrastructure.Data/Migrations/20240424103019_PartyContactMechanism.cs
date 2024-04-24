using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtterlyComplete.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PartyContactMechanism : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMechanismParty");

            migrationBuilder.AddColumn<int>(
                name: "SquareFootage",
                table: "Facilities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PartyContactMechanism",
                columns: table => new
                {
                    PartyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContactMechanismId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NonSolicitationIndicator = table.Column<bool>(type: "INTEGER", nullable: true),
                    Extension = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyContactMechanism", x => new { x.PartyId, x.ContactMechanismId, x.FromDate });
                    table.ForeignKey(
                        name: "FK_PartyContactMechanism_ContactMechanisms_ContactMechanismId",
                        column: x => x.ContactMechanismId,
                        principalTable: "ContactMechanisms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyContactMechanism_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyContactMechanism_ContactMechanismId",
                table: "PartyContactMechanism",
                column: "ContactMechanismId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyContactMechanism");

            migrationBuilder.DropColumn(
                name: "SquareFootage",
                table: "Facilities");

            migrationBuilder.CreateTable(
                name: "ContactMechanismParty",
                columns: table => new
                {
                    ContactMechanismsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartiesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMechanismParty", x => new { x.ContactMechanismsId, x.PartiesId });
                    table.ForeignKey(
                        name: "FK_ContactMechanismParty_ContactMechanisms_ContactMechanismsId",
                        column: x => x.ContactMechanismsId,
                        principalTable: "ContactMechanisms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactMechanismParty_Parties_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactMechanismParty_PartiesId",
                table: "ContactMechanismParty",
                column: "PartiesId");
        }
    }
}
