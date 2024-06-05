using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement_API.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CSDSDocument",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CSDSDocument");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument",
                column: "PropertyId");
        }
    }
}
