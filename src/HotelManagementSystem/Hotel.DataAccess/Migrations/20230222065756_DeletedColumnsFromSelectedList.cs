#nullable disable

namespace Hotel.DataAccess.Migrations
{
    public partial class DeletedColumnsFromSelectedList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlatCount",
                table: "SelectedLists");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SelectedLists");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "FlatCount",
                table: "SelectedLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "SelectedLists",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
