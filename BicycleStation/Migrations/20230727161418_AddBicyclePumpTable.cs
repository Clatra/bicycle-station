using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BicycleStation.Migrations
{
    /// <inheritdoc />
    public partial class AddBicyclePumpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPump",
                table: "RepairStations");

            migrationBuilder.CreateTable(
                name: "BicyclePump",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RepairStationId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BicyclePump", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BicyclePump_RepairStations_RepairStationId",
                        column: x => x.RepairStationId,
                        principalTable: "RepairStations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BicyclePump_RepairStationId",
                table: "BicyclePump",
                column: "RepairStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BicyclePump");

            migrationBuilder.AddColumn<bool>(
                name: "HasPump",
                table: "RepairStations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
