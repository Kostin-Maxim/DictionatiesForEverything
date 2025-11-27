using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionatiesForEverything.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStructureDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlossaryItemImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItmeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    GlossaryItemId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlossaryItemImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlossaryItemImages_GlossaryItems_GlossaryItemId",
                        column: x => x.GlossaryItemId,
                        principalTable: "GlossaryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlossaryItemImages_GlossaryItemId",
                table: "GlossaryItemImages",
                column: "GlossaryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlossaryItemImages");
        }
    }
}
