using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelManagement.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Person_ReservationPersonpersonID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_ReservationRoomIDRoom",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationPersonpersonID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationRoomIDRoom",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationPersonpersonID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationRoomIDRoom",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "activeReservation",
                table: "Reservations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "reservationPersonID",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "reservationRoomID",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_reservationPersonID",
                table: "Reservations",
                column: "reservationPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_reservationRoomID",
                table: "Reservations",
                column: "reservationRoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Person_reservationPersonID",
                table: "Reservations",
                column: "reservationPersonID",
                principalTable: "Person",
                principalColumn: "personID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_reservationRoomID",
                table: "Reservations",
                column: "reservationRoomID",
                principalTable: "Rooms",
                principalColumn: "IDRoom",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Person_reservationPersonID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_reservationRoomID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_reservationPersonID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_reservationRoomID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "activeReservation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "reservationPersonID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "reservationRoomID",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationPersonpersonID",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationRoomIDRoom",
                table: "Reservations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationPersonpersonID",
                table: "Reservations",
                column: "ReservationPersonpersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationRoomIDRoom",
                table: "Reservations",
                column: "ReservationRoomIDRoom");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Person_ReservationPersonpersonID",
                table: "Reservations",
                column: "ReservationPersonpersonID",
                principalTable: "Person",
                principalColumn: "personID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_ReservationRoomIDRoom",
                table: "Reservations",
                column: "ReservationRoomIDRoom",
                principalTable: "Rooms",
                principalColumn: "IDRoom",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
