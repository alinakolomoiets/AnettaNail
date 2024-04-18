using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnettaNail.Migrations
{
    /// <inheritdoc />
    public partial class updateKasutajad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roll",
                columns: table => new
                {
                    RollId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roll", x => x.RollId);
                });

            migrationBuilder.CreateTable(
                name: "Kasutajad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefoniNumber = table.Column<int>(type: "int", nullable: false),
                    RollId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kasutajad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kasutajad_Roll_RollId",
                        column: x => x.RollId,
                        principalTable: "Roll",
                        principalColumn: "RollId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kasutajad_RollId",
                table: "Kasutajad",
                column: "RollId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kasutajad");

            migrationBuilder.DropTable(
                name: "Roll");
        }
    }
}
