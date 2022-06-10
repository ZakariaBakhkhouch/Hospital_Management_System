using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S.G.H.Migrations
{
    public partial class AddDateDepartColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDépart",
                table: "Factures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDépart",
                table: "Factures");
        }
    }
}
