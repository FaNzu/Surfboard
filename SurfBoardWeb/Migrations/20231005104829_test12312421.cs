using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardWeb.Migrations
{
    /// <inheritdoc />
    public partial class test12312421 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Board_BoardId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BoardId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookings",
                newName: "BookingsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Board",
                newName: "BoardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookingsId",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Board",
                newName: "Id");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Bookings",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BoardId",
                table: "Bookings",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Board_BoardId",
                table: "Bookings",
                column: "BoardId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
