using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBS.ConsumerServicesAPI.Migrations
{
    public partial class SeedTicketData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassangerName",
                table: "Tickets",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "IsDeleted", "PassangerName", "PurchaseDate", "SeatNumber", "TripId" },
                values: new object[] { 1, false, "Levan Jintcharadze", new DateTime(2020, 2, 5, 17, 0, 0, 0, DateTimeKind.Unspecified), 20, 7 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "IsDeleted", "PassangerName", "PurchaseDate", "SeatNumber", "TripId" },
                values: new object[] { 2, false, "Dimitri Dondoladze", new DateTime(2020, 2, 7, 19, 0, 0, 0, DateTimeKind.Unspecified), 14, 3 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "IsDeleted", "PassangerName", "PurchaseDate", "SeatNumber", "TripId" },
                values: new object[] { 3, false, "Zura Kavtaradze", new DateTime(2020, 2, 8, 21, 0, 0, 0, DateTimeKind.Unspecified), 33, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PassangerName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
