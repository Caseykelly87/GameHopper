using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameHopper.Migrations
{
    /// <inheritdoc />
    public partial class addidentiy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "AspNetUsers",
                newName: "user_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_type",
                table: "AspNetUsers",
                newName: "Discriminator");
        }
    }
}
