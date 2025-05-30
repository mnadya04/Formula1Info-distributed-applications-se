using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1Info.Migrations
{
    public partial class AddUniqueIndexToDriverNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Drivers_DriverId",
                table: "Races");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Drivers_DriverId",
                table: "Races",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Drivers_DriverId",
                table: "Races");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Drivers_DriverId",
                table: "Races",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId");
        }
    }
}
