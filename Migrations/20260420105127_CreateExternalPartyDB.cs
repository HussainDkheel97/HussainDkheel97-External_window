using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace External_Party.Migrations
{
    /// <inheritdoc />
    public partial class CreateExternalPartyDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "External_Parties",
                columns: table => new
                {
                    External_Party_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    External_Party_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_External_Parties", x => x.External_Party_ID);
                });

            migrationBuilder.CreateTable(
                name: "External_Party_Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false),
                    External_Party_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_External_Party_Users", x => x.User_ID);
                    table.ForeignKey(
                        name: "FK_External_Party_Users_External_Parties_External_Party_ID",
                        column: x => x.External_Party_ID,
                        principalTable: "External_Parties",
                        principalColumn: "External_Party_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mail_Of_Externals",
                columns: table => new
                {
                    ExMailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mail_Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Of_Mail = table.Column<DateTime>(type: "datetime2", nullable: false),
                    clasification = table.Column<int>(type: "int", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false),
                    External_Party_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail_Of_Externals", x => x.ExMailID);
                    table.ForeignKey(
                        name: "FK_Mail_Of_Externals_External_Parties_External_Party_ID",
                        column: x => x.External_Party_ID,
                        principalTable: "External_Parties",
                        principalColumn: "External_Party_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_External_Party_Users_External_Party_ID",
                table: "External_Party_Users",
                column: "External_Party_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_Of_Externals_External_Party_ID",
                table: "Mail_Of_Externals",
                column: "External_Party_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "External_Party_Users");

            migrationBuilder.DropTable(
                name: "Mail_Of_Externals");

            migrationBuilder.DropTable(
                name: "External_Parties");
        }
    }
}
