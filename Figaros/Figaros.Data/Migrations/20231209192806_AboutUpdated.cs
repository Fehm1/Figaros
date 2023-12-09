using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figaros.Data.Migrations
{
    public partial class AboutUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurSalon");

            migrationBuilder.RenameColumn(
                name: "ImageString",
                table: "About",
                newName: "SmallImageString");

            migrationBuilder.AddColumn<string>(
                name: "BigImageString",
                table: "About",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigImageString",
                table: "About");

            migrationBuilder.RenameColumn(
                name: "SmallImageString",
                table: "About",
                newName: "ImageString");

            migrationBuilder.CreateTable(
                name: "OurSalon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageString = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LittleTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurSalon", x => x.Id);
                });
        }
    }
}
