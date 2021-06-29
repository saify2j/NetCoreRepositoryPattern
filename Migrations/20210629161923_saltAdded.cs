using Microsoft.EntityFrameworkCore.Migrations;

namespace MessagingRealtime.Migrations
{
    public partial class saltAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecretSalt",
                table: "AppUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretSalt",
                table: "AppUsers");
        }
    }
}
