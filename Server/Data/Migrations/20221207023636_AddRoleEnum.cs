using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Renovation.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "AspNetUsers",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "etat",
                table: "AspNetUsers",
                newName: "Etat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AspNetUsers",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Etat",
                table: "AspNetUsers",
                newName: "etat");
        }
    }
}
