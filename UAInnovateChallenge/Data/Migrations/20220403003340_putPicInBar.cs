using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UAInnovateChallenge.Data.Migrations
{
    public partial class putPicInBar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarPictures");

            migrationBuilder.AddColumn<byte[]>(
                name: "BarPicture",
                table: "Bar",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarPicture",
                table: "Bar");

            migrationBuilder.CreateTable(
                name: "BarPictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarPictures_Bar_BarId",
                        column: x => x.BarId,
                        principalTable: "Bar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarPictures_BarId",
                table: "BarPictures",
                column: "BarId");
        }
    }
}
