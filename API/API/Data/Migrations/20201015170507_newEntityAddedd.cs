using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class newEntityAddedd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddedBy = table.Column<int>(nullable: false),
                    CarType = table.Column<string>(nullable: true),
                    PlateNumber = table.Column<string>(nullable: true),
                    InspectedAt = table.Column<DateTime>(nullable: false),
                    InspectedUntil = table.Column<DateTime>(nullable: false),
                    InspectionInterval = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    OwnerTelNr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
