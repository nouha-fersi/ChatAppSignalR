using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoChatApp.Migrations
{
    /// <inheritdoc />
    public partial class _4thupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReciverId",
                table: "IndividualChats");

            migrationBuilder.DropColumn(
                name: "ReciverName",
                table: "IndividualChats");

            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "IndividualChats",
                newName: "ReceiverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "IndividualChats",
                newName: "SenderName");

            migrationBuilder.AddColumn<string>(
                name: "ReciverId",
                table: "IndividualChats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReciverName",
                table: "IndividualChats",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
