using Microsoft.EntityFrameworkCore.Migrations;

namespace S.G.H.Migrations
{
    public partial class AddDateEntreeColun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateEntrée",
                table: "Patients",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PatientMatricule",
                table: "Chambres",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEntrée",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "PatientMatricule",
                table: "Chambres",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
