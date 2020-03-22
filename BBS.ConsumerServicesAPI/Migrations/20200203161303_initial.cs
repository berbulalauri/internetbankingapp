using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBS.ConsumerServicesAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bs");

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                schema: "bs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatsCount = table.Column<int>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Departure = table.Column<DateTime>(nullable: false),
                    DestinationToId = table.Column<int>(nullable: false),
                    DestinationFromId = table.Column<int>(nullable: false),
                    BusId = table.Column<int>(nullable: false),
                    TravelTime = table.Column<TimeSpan>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Buses_BusId",
                        column: x => x.BusId,
                        principalSchema: "bs",
                        principalTable: "Buses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trips_Destinations_DestinationFromId",
                        column: x => x.DestinationFromId,
                        principalTable: "Destinations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trips_Destinations_DestinationToId",
                        column: x => x.DestinationToId,
                        principalTable: "Destinations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassangerName = table.Column<string>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    SeatNumber = table.Column<int>(nullable: false),
                    TripId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Address", "IsDeleted", "Name" },
                values: new object[] { 1, "Tbilisi bus station", false, "Tbilisi" });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Address", "IsDeleted", "Name" },
                values: new object[] { 2, "Batumi bus station", false, "Batumi" });

            migrationBuilder.InsertData(
                schema: "bs",
                table: "Buses",
                columns: new[] { "Id", "IsDeleted", "Model", "SeatsCount" },
                values: new object[] { 1, false, "Vacanza 13", 52 });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "BusId", "Departure", "DestinationFromId", "DestinationToId", "IsDeleted", "TicketPrice", "TravelTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 2, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, 10m, new TimeSpan(0, 7, 0, 0, 0) },
                    { 2, 1, new DateTime(2020, 2, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, 10m, new TimeSpan(0, 7, 0, 0, 0) },
                    { 3, 1, new DateTime(2020, 2, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, 10m, new TimeSpan(0, 7, 0, 0, 0) },
                    { 4, 1, new DateTime(2020, 2, 4, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, 10m, new TimeSpan(0, 7, 0, 0, 0) },
                    { 5, 1, new DateTime(2020, 2, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, 10m, new TimeSpan(0, 7, 0, 0, 0) },
                    { 6, 1, new DateTime(2020, 2, 4, 16, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, false, 10m, new TimeSpan(0, 7, 20, 0, 0) },
                    { 7, 1, new DateTime(2020, 2, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, false, 10m, new TimeSpan(0, 7, 20, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TripId",
                table: "Tickets",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BusId",
                table: "Trips",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationFromId",
                table: "Trips",
                column: "DestinationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationToId",
                table: "Trips",
                column: "DestinationToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Buses",
                schema: "bs");

            migrationBuilder.DropTable(
                name: "Destinations");
        }
    }
}
