using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtterlyComplete.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FacilityRoleTypeDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FacilityRoleType",
                keyColumn: "Id",
                keyValue: 0,
                column: "Value",
                value: "UNKNOWN");

            migrationBuilder.UpdateData(
                table: "FacilityRoleType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "OWNED");

            migrationBuilder.UpdateData(
                table: "FacilityRoleType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Value",
                value: "LEASED");

            migrationBuilder.InsertData(
                table: "FacilityRoleType",
                columns: new[] { "Id", "Value" },
                values: new object[] { 3, "BOOKED" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FacilityRoleType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "FacilityRoleType",
                keyColumn: "Id",
                keyValue: 0,
                column: "Value",
                value: "OWNED");

            migrationBuilder.UpdateData(
                table: "FacilityRoleType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "LEASED");

            migrationBuilder.UpdateData(
                table: "FacilityRoleType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Value",
                value: "BOOKED");
        }
    }
}
