using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordZone.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PacketName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnglishWord = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PolishTranslation = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PacketID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Translations_Packets_PacketID",
                        column: x => x.PacketID,
                        principalTable: "Packets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translations_PacketID",
                table: "Translations",
                column: "PacketID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Packets");
        }
    }
}
