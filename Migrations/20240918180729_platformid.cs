using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace re.colocar.me.talent.Migrations
{
    public partial class platformid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Users");
        }
    }
}
