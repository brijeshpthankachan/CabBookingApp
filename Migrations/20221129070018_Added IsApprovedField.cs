using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsApprovedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLineOneHouseNameOrHouseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLineTwoDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddresLineThreeLocality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddresLineFourState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddresLineFivePin = table.Column<int>(type: "int", nullable: false),
                    AadharNumber = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoGraph = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CabInfosId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApprovedToDrive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverInfos_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverInfosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CabInfos_DriverInfos_DriverInfosID",
                        column: x => x.DriverInfosID,
                        principalTable: "DriverInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabInfos_DriverInfosID",
                table: "CabInfos",
                column: "DriverInfosID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverInfos_ApplicationUsersId",
                table: "DriverInfos",
                column: "ApplicationUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabInfos");

            migrationBuilder.DropTable(
                name: "DriverInfos");
        }
    }
}
