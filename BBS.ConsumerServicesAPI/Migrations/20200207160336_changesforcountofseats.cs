using Microsoft.EntityFrameworkCore.Migrations;

namespace BBS.ConsumerServicesAPI.Migrations
{
    public partial class changesforcountofseats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatsCount",
                schema: "bs",
                table: "Buses");

            migrationBuilder.AddColumn<int>(
                name: "CountOfSeats",
                schema: "bs",
                table: "Buses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "bs",
                table: "Buses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CountOfSeats",
                value: 52);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfSeats",
                schema: "bs",
                table: "Buses");

            migrationBuilder.AddColumn<int>(
                name: "SeatsCount",
                schema: "bs",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "bs",
                table: "Buses",
                keyColumn: "Id",
                keyValue: 1,
                column: "SeatsCount",
                value: 52);
        }
    }
}
