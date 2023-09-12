using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardWeb.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultUserId",
                table: "Board",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Board",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Board",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Board_DefaultUserId",
                table: "Board",
                column: "DefaultUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Board_AspNetUsers_DefaultUserId",
                table: "Board",
                column: "DefaultUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Board_AspNetUsers_DefaultUserId",
                table: "Board");

            migrationBuilder.DropIndex(
                name: "IX_Board_DefaultUserId",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "DefaultUserId",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Board");
        }
    }
}
