using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace re.colocar.me.talent.Migrations
{
    public partial class alternotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Owner",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Notifications");
        }
    }
}
