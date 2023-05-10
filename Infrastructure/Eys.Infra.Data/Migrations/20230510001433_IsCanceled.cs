using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eys.Infra.Data.Migrations
{
    public partial class IsCanceled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Events");
        }
    }
}
