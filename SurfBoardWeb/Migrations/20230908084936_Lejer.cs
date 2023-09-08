using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardWeb.Migrations
{
    /// <inheritdoc />
    public partial class Lejer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lejer",
                table: "Board",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SlutDate",
                table: "Board",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Board",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lejer",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "SlutDate",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Board");
        }
    }
}
