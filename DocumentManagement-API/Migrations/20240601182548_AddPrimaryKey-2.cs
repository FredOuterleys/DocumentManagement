using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement_API.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CSDSDocument",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument",
                columns: new[] { "PropertyId", "Filename" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CSDSDocument",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CSDSDocument",
                table: "CSDSDocument",
                column: "Id");
        }
    }
}
