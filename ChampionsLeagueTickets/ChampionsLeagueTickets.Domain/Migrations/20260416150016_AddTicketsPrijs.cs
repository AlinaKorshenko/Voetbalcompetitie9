using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeagueTickets.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketsPrijs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seizoenen",
                columns: table => new
                {
                    seizoenID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    naam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    startDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    eindDatum = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seizoene__C0EC0D7B16A4F77F", x => x.seizoenID);
                });

            migrationBuilder.CreateTable(
                name: "Stadions",
                columns: table => new
                {
                    stadionID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    naam = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    land = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    adres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    postcode = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    gemeente = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stadions__56AECC45BAFDAEA9", x => x.stadionID);
                });

            migrationBuilder.CreateTable(
                name: "TicketsPrijs",
                columns: table => new
                {
                    MatchId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VakNummer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsPrijs", x => new { x.MatchId, x.VakNummer });
                });

            migrationBuilder.CreateTable(
                name: "VakTypes",
                columns: table => new
                {
                    vakNummer = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    ring = table.Column<int>(type: "int", nullable: false),
                    omschrijving = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VakTypes__BBCA469A4C59D109", x => x.vakNummer);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    userID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    datumTijdOrder = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__0809337D408CD464", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    teamID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    naam = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    stadionID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teams__5ED7534A07296CA1", x => x.teamID);
                    table.ForeignKey(
                        name: "fk_StadionID_Teams",
                        column: x => x.stadionID,
                        principalTable: "Stadions",
                        principalColumn: "stadionID");
                });

            migrationBuilder.CreateTable(
                name: "Zitplaatsen",
                columns: table => new
                {
                    stadionID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    zitplaatsID = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    vakNummer = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    rijNummer = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    stoelNummer = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Zitplaat__76B42E8743521A35", x => new { x.stadionID, x.zitplaatsID });
                    table.ForeignKey(
                        name: "fk_stadionID_Zitplaatsen",
                        column: x => x.stadionID,
                        principalTable: "Stadions",
                        principalColumn: "stadionID");
                    table.ForeignKey(
                        name: "fk_vakNummer_Zitplaatsen",
                        column: x => x.vakNummer,
                        principalTable: "VakTypes",
                        principalColumn: "vakNummer");
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    matchID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    thuisTeamID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    bezoekendTeamID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    datumTijdStartMatch = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Matches__02C72A2D6D040317", x => x.matchID);
                    table.ForeignKey(
                        name: "fk_bezoekendTeamID",
                        column: x => x.bezoekendTeamID,
                        principalTable: "Teams",
                        principalColumn: "teamID");
                    table.ForeignKey(
                        name: "fk_thuisTeamID",
                        column: x => x.thuisTeamID,
                        principalTable: "Teams",
                        principalColumn: "teamID");
                });

            migrationBuilder.CreateTable(
                name: "Abonnementen",
                columns: table => new
                {
                    abonnementID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    stadionID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    userID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    zitplaatsID = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    startDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    eindDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    seizoenID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Abonneme__CF3D39B38C8AC8F7", x => new { x.abonnementID, x.stadionID });
                    table.ForeignKey(
                        name: "FK_Abonnementen_AspNetUsers",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_seizoenID_Abonnementen",
                        column: x => x.seizoenID,
                        principalTable: "Seizoenen",
                        principalColumn: "seizoenID");
                    table.ForeignKey(
                        name: "fk_stadionID_Abonnementen",
                        column: x => x.stadionID,
                        principalTable: "Stadions",
                        principalColumn: "stadionID");
                    table.ForeignKey(
                        name: "fk_zitplaatsID_Abonnementen",
                        columns: x => new { x.stadionID, x.zitplaatsID },
                        principalTable: "Zitplaatsen",
                        principalColumns: new[] { "stadionID", "zitplaatsID" });
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticketID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    matchID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    zitplaatsID = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    stadionID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    prijs = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tickets__F31FB4D279B38C40", x => new { x.ticketID, x.matchID });
                    table.ForeignKey(
                        name: "fk_matchID_Tickets",
                        column: x => x.matchID,
                        principalTable: "Matches",
                        principalColumn: "matchID");
                    table.ForeignKey(
                        name: "fk_stadionID_Tickets",
                        column: x => x.stadionID,
                        principalTable: "Stadions",
                        principalColumn: "stadionID");
                    table.ForeignKey(
                        name: "fk_zitplaatsID_Tickets",
                        columns: x => new { x.stadionID, x.zitplaatsID },
                        principalTable: "Zitplaatsen",
                        principalColumns: new[] { "stadionID", "zitplaatsID" });
                });

            migrationBuilder.CreateTable(
                name: "Orderlijnen",
                columns: table => new
                {
                    orderID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    orderLijnNummer = table.Column<int>(type: "int", nullable: false),
                    abonnementID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    stadionID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    ticketID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    matchID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    bedrag = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orderlij__BEA8F4E8FF672866", x => new { x.orderID, x.orderLijnNummer });
                    table.ForeignKey(
                        name: "fk_abonnement_Orderlijnen",
                        columns: x => new { x.abonnementID, x.stadionID },
                        principalTable: "Abonnementen",
                        principalColumns: new[] { "abonnementID", "stadionID" });
                    table.ForeignKey(
                        name: "fk_orderID_Orderlijnen",
                        column: x => x.orderID,
                        principalTable: "Orders",
                        principalColumn: "orderID");
                    table.ForeignKey(
                        name: "fk_ticket_Orderlijnen",
                        columns: x => new { x.ticketID, x.matchID },
                        principalTable: "Tickets",
                        principalColumns: new[] { "ticketID", "matchID" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnementen_seizoenID",
                table: "Abonnementen",
                column: "seizoenID");

            migrationBuilder.CreateIndex(
                name: "IX_Abonnementen_stadionID_zitplaatsID",
                table: "Abonnementen",
                columns: new[] { "stadionID", "zitplaatsID" });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnementen_userID",
                table: "Abonnementen",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_bezoekendTeamID",
                table: "Matches",
                column: "bezoekendTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_thuisTeamID",
                table: "Matches",
                column: "thuisTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Orderlijnen_abonnementID_stadionID",
                table: "Orderlijnen",
                columns: new[] { "abonnementID", "stadionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Orderlijnen_ticketID_matchID",
                table: "Orderlijnen",
                columns: new[] { "ticketID", "matchID" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userID",
                table: "Orders",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_stadionID",
                table: "Teams",
                column: "stadionID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_matchID",
                table: "Tickets",
                column: "matchID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_stadionID_zitplaatsID",
                table: "Tickets",
                columns: new[] { "stadionID", "zitplaatsID" });

            migrationBuilder.CreateIndex(
                name: "IX_Zitplaatsen_vakNummer",
                table: "Zitplaatsen",
                column: "vakNummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "Orderlijnen");

            migrationBuilder.DropTable(
                name: "TicketsPrijs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Abonnementen");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Seizoenen");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Zitplaatsen");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "VakTypes");

            migrationBuilder.DropTable(
                name: "Stadions");
        }
    }
}
