using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuRestaurantWebAPP.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class InitialMenuMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_portate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipologia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__portate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_pietanze",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false),
                    PortataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipologia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pietanze", x => x.Id);
                    table.ForeignKey(
                        name: "FK__pietanze__portate_PortataId",
                        column: x => x.PortataId,
                        principalTable: "_portate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__pietanze_PortataId",
                table: "_pietanze",
                column: "PortataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_pietanze");

            migrationBuilder.DropTable(
                name: "_portate");
        }
    }
}
