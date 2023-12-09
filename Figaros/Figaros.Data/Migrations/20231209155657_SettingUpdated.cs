using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figaros.Data.Migrations
{
    public partial class SettingUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactImageString",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "FooterDescription",
                table: "Settings",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OurBarber",
                table: "Settings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OurBarberDescription",
                table: "Settings",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OurPrice",
                table: "Settings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OurPriceDescription",
                table: "Settings",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OurService",
                table: "Settings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OurServiceDescription",
                table: "Settings",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterDescription",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurBarber",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurBarberDescription",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurPrice",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurPriceDescription",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurService",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurServiceDescription",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "ContactImageString",
                table: "Settings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
