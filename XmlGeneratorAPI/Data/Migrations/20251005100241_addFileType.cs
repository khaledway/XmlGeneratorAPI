using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XmlGeneratorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class addFileType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "FilesUpload",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "FilesUpload");
        }
    }
}
