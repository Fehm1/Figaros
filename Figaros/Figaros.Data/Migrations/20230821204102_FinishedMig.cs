using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figaros.Data.Migrations
{
    public partial class FinishedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Services_ServiceId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Bookings",
                newName: "TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ServiceId",
                table: "Bookings",
                newName: "IX_Bookings_TimeId");

            migrationBuilder.AddColumn<string>(
                name: "FooterLogo",
                table: "Settings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeaderLogo",
                table: "Settings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActiceRequest",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPoster",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IntagramURl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TiktokUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PriceId",
                table: "Bookings",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Prices_PriceId",
                table: "Bookings",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Times_TimeId",
                table: "Bookings",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Prices_PriceId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Times_TimeId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PriceId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FooterLogo",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "HeaderLogo",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsActiceRequest",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsPoster",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "Bookings",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_TimeId",
                table: "Bookings",
                newName: "IX_Bookings_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Bookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Services_ServiceId",
                table: "Bookings",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
