using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldFinder.DAL.Migrations
{
    public partial class AddingLocationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Fields");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_LocationId",
                table: "Fields",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Locations_LocationId",
                table: "Fields",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Locations_LocationId",
                table: "Fields");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Fields_LocationId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
