using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CSDSDocument",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "TEXT", nullable: false),
                    CaseNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Filename = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSDSDocument", x => x.PropertyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CSDSDocument");
        }
    }
}
