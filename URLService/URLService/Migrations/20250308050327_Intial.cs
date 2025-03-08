using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLService.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "URLs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LongURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortenPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "URLAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URLId = table.Column<int>(type: "int", nullable: false),
                    AccessedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLAnalytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_URLAnalytics_URLs_URLId",
                        column: x => x.URLId,
                        principalTable: "URLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_URLAnalytics_URLId",
                table: "URLAnalytics",
                column: "URLId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "URLAnalytics");

            migrationBuilder.DropTable(
                name: "URLs");
        }
    }
}
