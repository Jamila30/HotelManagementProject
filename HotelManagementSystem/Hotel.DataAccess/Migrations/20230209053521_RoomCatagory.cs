using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    public partial class RoomCatagory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomCatagoryId",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomCatagories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCatagories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flats_RoomCatagoryId",
                table: "Flats",
                column: "RoomCatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_RoomCatagories_RoomCatagoryId",
                table: "Flats",
                column: "RoomCatagoryId",
                principalTable: "RoomCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flats_RoomCatagories_RoomCatagoryId",
                table: "Flats");

            migrationBuilder.DropTable(
                name: "RoomCatagories");

            migrationBuilder.DropIndex(
                name: "IX_Flats_RoomCatagoryId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "RoomCatagoryId",
                table: "Flats");
        }
    }
}
