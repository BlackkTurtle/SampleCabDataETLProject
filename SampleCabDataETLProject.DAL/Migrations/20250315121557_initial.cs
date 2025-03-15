using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCabDataETLProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CabDataEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tpep_pickup_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tpep_dropoff_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    passenger_count = table.Column<int>(type: "int", nullable: false),
                    trip_distance = table.Column<double>(type: "float", nullable: false),
                    store_and_fwd_flag = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    PULocationID = table.Column<int>(type: "int", nullable: false),
                    DOLocationID = table.Column<int>(type: "int", nullable: false),
                    fare_amount = table.Column<double>(type: "float", nullable: false),
                    tip_amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabDataEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabDataEntities");
        }
    }
}
