using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eys.Infra.Data.Migrations
{
    public partial class UploadedImageAddOrderCoverPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoverPhoto",
                table: "EventImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsCoverPhoto",
                table: "UploadedImage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "UploadedImage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoverPhoto",
                table: "UploadedImage");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "UploadedImage");

            migrationBuilder.AddColumn<bool>(
                name: "IsCoverPhoto",
                table: "EventImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
