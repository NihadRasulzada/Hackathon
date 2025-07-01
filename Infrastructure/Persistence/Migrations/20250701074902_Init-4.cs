using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationService_Reservations_ReservationsId",
                table: "ReservationService");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationService_Services_ServicesId",
                table: "ReservationService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationService",
                table: "ReservationService");

            migrationBuilder.RenameColumn(
                name: "ServicesId",
                table: "ReservationService",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "ReservationsId",
                table: "ReservationService",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationService_ServicesId",
                table: "ReservationService",
                newName: "IX_ReservationService_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ReservationService",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ReservationService",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationService",
                table: "ReservationService",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationService_ReservationId",
                table: "ReservationService",
                column: "ReservationId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationService_Reservations_ReservationId",
                table: "ReservationService");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationService_Services_ServiceId",
                table: "ReservationService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationService",
                table: "ReservationService");

            migrationBuilder.DropIndex(
                name: "IX_ReservationService_ReservationId",
                table: "ReservationService");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReservationService");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ReservationService");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "ReservationService",
                newName: "ServicesId");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "ReservationService",
                newName: "ReservationsId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationService_ServiceId",
                table: "ReservationService",
                newName: "IX_ReservationService_ServicesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationService",
                table: "ReservationService",
                columns: new[] { "ReservationsId", "ServicesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationService_Reservations_ReservationsId",
                table: "ReservationService",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationService_Services_ServicesId",
                table: "ReservationService",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
