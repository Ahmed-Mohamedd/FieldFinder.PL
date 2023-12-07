using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldFinder.DAL.Migrations
{
    public partial class ModifingImageColumnNameToImageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Fields",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Fields",
                newName: "Image");
        }
    }
}
