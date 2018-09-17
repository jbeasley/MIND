using Microsoft.EntityFrameworkCore.Migrations;

namespace SigmaCloudManager.Migrations
{
    public partial class update18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Device",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Device",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
