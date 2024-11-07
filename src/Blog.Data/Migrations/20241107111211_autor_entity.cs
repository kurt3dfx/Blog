using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class autor_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "teste",
                table: "Comentarios",
                newName: "Id_autor");

            migrationBuilder.AddColumn<string>(
                name: "Id_autor",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_autor",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Id_autor",
                table: "Comentarios",
                newName: "teste");
        }
    }
}
