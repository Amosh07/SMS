using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.Migrators.PostgresSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class Class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Classes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
