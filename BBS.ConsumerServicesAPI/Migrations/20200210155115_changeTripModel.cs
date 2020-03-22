using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBS.ConsumerServicesAPI.Migrations
{
    public partial class changeTripModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelTime",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "TravelTime1",
                table: "Trips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "TravelTime1",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "TravelTime1",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                column: "TravelTime1",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4,
                column: "TravelTime1",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 5,
                column: "TravelTime1",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 6,
                column: "TravelTime1",
                value: 440);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 7,
                column: "TravelTime1",
                value: 440);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelTime1",
                table: "Trips");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TravelTime",
                table: "Trips",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "TravelTime",
                value: new TimeSpan(0, 7, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "TravelTime",
                value: new TimeSpan(0, 7, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                column: "TravelTime",
                value: new TimeSpan(0, 7, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4,
                column: "TravelTime",
                value: new TimeSpan(0, 7, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 5,
                column: "TravelTime",
                value: new TimeSpan(0, 7, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 6,
                column: "TravelTime",
                value: new TimeSpan(0, 7, 20, 0, 0));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 7,
                column: "TravelTime",
                value: new TimeSpan(0, 7, 20, 0, 0));
        }
    }
}
