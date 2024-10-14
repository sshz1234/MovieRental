using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2084Assign2Real.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    MovieRentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    owingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRentalId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    rentalDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.MovieRentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_userRental_UserRentalId",
                        column: x => x.UserRentalId,
                        principalTable: "userRental",
                        principalColumn: "UserRentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserRentalId",
                table: "Rentals",
                column: "UserRentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "userRental");
        }
    }
}
