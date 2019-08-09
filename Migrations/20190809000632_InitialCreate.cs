using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    IDHotel = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nameHotel = table.Column<string>(nullable: false),
                    locationHotel = table.Column<string>(nullable: false),
                    availableHotel = table.Column<bool>(nullable: false),
                    addressHotel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.IDHotel);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    personID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    personIdentification = table.Column<int>(nullable: false),
                    personName = table.Column<string>(nullable: false),
                    personTypeDocument = table.Column<string>(nullable: false),
                    personDateBirth = table.Column<DateTime>(nullable: false),
                    personGender = table.Column<string>(nullable: false),
                    personEmail = table.Column<string>(nullable: false),
                    personPhone = table.Column<string>(nullable: false),
                    personEmergencyContactName = table.Column<string>(nullable: false),
                    personEmergencyContactPhone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.personID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    IDRoom = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    oRoomIDHotel = table.Column<int>(nullable: true),
                    IDRoomHotel = table.Column<int>(nullable: false),
                    availableRoom = table.Column<bool>(nullable: false),
                    roomName = table.Column<string>(nullable: false),
                    totalPriceRoom = table.Column<decimal>(nullable: false),
                    quantityRoom = table.Column<int>(nullable: false),
                    taxesPercentRooms = table.Column<int>(nullable: false),
                    basePriceRoom = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.IDRoom);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_oRoomIDHotel",
                        column: x => x.oRoomIDHotel,
                        principalTable: "Hotels",
                        principalColumn: "IDHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userNameUser = table.Column<string>(nullable: false),
                    UserPersonpersonID = table.Column<int>(nullable: false),
                    userNamePassword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userNameUser);
                    table.ForeignKey(
                        name: "FK_Users_Person_UserPersonpersonID",
                        column: x => x.UserPersonpersonID,
                        principalTable: "Person",
                        principalColumn: "personID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IDReservation = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReservationPersonpersonID = table.Column<int>(nullable: true),
                    ReservationRoomIDRoom = table.Column<int>(nullable: true),
                    phonePersonReservation = table.Column<string>(nullable: false),
                    sinceReservation = table.Column<DateTime>(nullable: false),
                    untilReservation = table.Column<DateTime>(nullable: false),
                    quantityPersonReservation = table.Column<int>(nullable: false),
                    descriptionReservation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IDReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_Person_ReservationPersonpersonID",
                        column: x => x.ReservationPersonpersonID,
                        principalTable: "Person",
                        principalColumn: "personID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_ReservationRoomIDRoom",
                        column: x => x.ReservationRoomIDRoom,
                        principalTable: "Rooms",
                        principalColumn: "IDRoom",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationPersonpersonID",
                table: "Reservations",
                column: "ReservationPersonpersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationRoomIDRoom",
                table: "Reservations",
                column: "ReservationRoomIDRoom");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_oRoomIDHotel",
                table: "Rooms",
                column: "oRoomIDHotel");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonpersonID",
                table: "Users",
                column: "UserPersonpersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
