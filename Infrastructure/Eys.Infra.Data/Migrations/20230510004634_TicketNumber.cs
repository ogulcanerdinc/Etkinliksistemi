using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eys.Infra.Data.Migrations
{
    public partial class TicketNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketNumber",
                table: "EventTickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketNumber",
                table: "EventTickets");
        }
    }
}
