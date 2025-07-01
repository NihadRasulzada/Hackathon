using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationService_Reservations_ReservationId",
                table: "ReservationService");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationService_Services_ServiceId",
                table: "ReservationService");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomId1",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationService",
                table: "ReservationService");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "ReservationService",
                newName: "ReservationServices");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationService_ServiceId",
                table: "ReservationServices",
                newName: "IX_ReservationServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationService_ReservationId",
                table: "ReservationServices",
                newName: "IX_ReservationServices_ReservationId");

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationServices",
                table: "ReservationServices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoomImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    RoomId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomImages_Rooms_RoomId1",
                        column: x => x.RoomId1,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImages_RoomId1",
                table: "RoomImages",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Services_ServiceId",
                table: "ReservationServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Services_ServiceId",
                table: "ReservationServices");

            migrationBuilder.DropTable(
                name: "RoomImages");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationServices",
                table: "ReservationServices");

            migrationBuilder.RenameTable(
                name: "ReservationServices",
                newName: "ReservationService");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationServices_ServiceId",
                table: "ReservationService",
                newName: "IX_ReservationService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationServices_ReservationId",
                table: "ReservationService",
                newName: "IX_ReservationService_ReservationId");

            migrationBuilder.AddColumn<string>(
                name: "ReservationId",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RoomId1",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationService",
                table: "ReservationService",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId1",
                table: "Reservations",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId1",
                table: "Reservations",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationService_Reservations_ReservationId",
                table: "ReservationService",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationService_Services_ServiceId",
                table: "ReservationService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
