using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionatiesForEverything.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Glossaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glossaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlossaryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    TypeIdId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlossaryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlossaryItems_Glossaries_TypeIdId",
                        column: x => x.TypeIdId,
                        principalTable: "Glossaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlossaryItems_TypeIdId",
                table: "GlossaryItems",
                column: "TypeIdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlossaryItems");

            migrationBuilder.DropTable(
                name: "Glossaries");
        }
    }
}
