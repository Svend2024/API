using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KameGameAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "logins",
                columns: table => new
                {
                    loginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logins", x => x.loginId);
                });

            migrationBuilder.CreateTable(
                name: "sets",
                columns: table => new
                {
                    setCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    setName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sets", x => x.setCode);
                });

            migrationBuilder.CreateTable(
                name: "transactionHistories",
                columns: table => new
                {
                    transactionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardId = table.Column<int>(type: "int", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionHistories", x => x.transactionHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "zipCodeCities",
                columns: table => new
                {
                    zipCode = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zipCodeCities", x => x.zipCode);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zipCode = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loginId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customerId);
                    table.ForeignKey(
                        name: "FK_customers_logins_loginId",
                        column: x => x.loginId,
                        principalTable: "logins",
                        principalColumn: "loginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productManagers",
                columns: table => new
                {
                    productManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loginId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productManagers", x => x.productManagerId);
                    table.ForeignKey(
                        name: "FK_productManagers_logins_loginId",
                        column: x => x.loginId,
                        principalTable: "logins",
                        principalColumn: "loginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    cardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    attribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cardCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    setCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    setCode1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pictureLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.cardId);
                    table.ForeignKey(
                        name: "FK_cards_sets_setCode1",
                        column: x => x.setCode1,
                        principalTable: "sets",
                        principalColumn: "setCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cards_setCode1",
                table: "cards",
                column: "setCode1");

            migrationBuilder.CreateIndex(
                name: "IX_customers_loginId",
                table: "customers",
                column: "loginId");

            migrationBuilder.CreateIndex(
                name: "IX_productManagers_loginId",
                table: "productManagers",
                column: "loginId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "productManagers");

            migrationBuilder.DropTable(
                name: "transactionHistories");

            migrationBuilder.DropTable(
                name: "zipCodeCities");

            migrationBuilder.DropTable(
                name: "sets");

            migrationBuilder.DropTable(
                name: "logins");
        }
    }
}
