using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Railway_Reservation.Migrations
{
    /// <inheritdoc />
    public partial class RailwayDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Train_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Train_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fare = table.Column<int>(type: "int", nullable: false),
                    Seat_Available = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Train_Id);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Booking_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    Train_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Seat_Booked = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Fare = table.Column<long>(type: "bigint", nullable: false),
                    Payment_Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Booking_Id);
                });

            migrationBuilder.CreateTable(
                name: "User2",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User2", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "User2");
        }
    }
}
