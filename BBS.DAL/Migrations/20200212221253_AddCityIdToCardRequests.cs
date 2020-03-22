using Microsoft.EntityFrameworkCore.Migrations;

namespace BBS.DAL.Migrations
{
    public partial class AddCityIdToCardRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardRequests_Cities_CityId",
                table: "CardRequests");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "CardRequests",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CardRequests_Cities_CityId",
                table: "CardRequests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardRequests_Cities_CityId",
                table: "CardRequests");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "CardRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CardRequests_Cities_CityId",
                table: "CardRequests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
