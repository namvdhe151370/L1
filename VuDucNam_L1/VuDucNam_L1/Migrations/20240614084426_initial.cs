using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VuDucNam_L1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Ethnics",
                columns: table => new
                {
                    EthnicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EthnicName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnics", x => x.EthnicId);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    WardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.WardId);
                    table.ForeignKey(
                        name: "FK_Wards_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    EthnicId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    CitizenNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    WardId = table.Column<int>(type: "int", nullable: false),
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Ethnics_EthnicId",
                        column: x => x.EthnicId,
                        principalTable: "Ethnics",
                        principalColumn: "EthnicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "WardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName" },
                values: new object[,]
                {
                    { 1, "Hà Nội" },
                    { 2, "Hồ Chí Minh" },
                    { 3, "Hải Phòng" },
                    { 4, "Đà Nẵng" },
                    { 5, "Cần Thơ" },
                    { 6, "Hà Giang" },
                    { 7, "Cao Bằng" },
                    { 8, "Lai Châu" },
                    { 9, "Lào Cai" },
                    { 10, "Lạng Sơn" }
                });

            migrationBuilder.InsertData(
                table: "Ethnics",
                columns: new[] { "EthnicId", "EthnicName" },
                values: new object[,]
                {
                    { 1, "Kinh" },
                    { 2, "Hoa" },
                    { 3, "Tày" },
                    { 4, "Mường" },
                    { 5, "Thái" },
                    { 6, "Khơ Me" },
                    { 7, "Nùng" },
                    { 8, "H'Mông" },
                    { 9, "Dao" },
                    { 10, "Gia Rai" },
                    { 11, "Ê Đê" },
                    { 12, "Ba Na" },
                    { 13, "Xơ Đăng" },
                    { 14, "Sán Chay" },
                    { 15, "Cơ Tu" },
                    { 16, "Chăm" },
                    { 17, "Sán Dìu" },
                    { 18, "Hrê" },
                    { 19, "Ra Glai" },
                    { 20, "M'nông" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "JobName" },
                values: new object[,]
                {
                    { 1, "Developer" },
                    { 2, "Teacher" },
                    { 3, "Doctor" },
                    { 4, "Engineer" },
                    { 5, "Nurse" },
                    { 6, "Accountant" },
                    { 7, "Lawyer" },
                    { 8, "Artist" },
                    { 9, "Chef" },
                    { 10, "Pilot" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictId", "CityId", "DistrictName" },
                values: new object[,]
                {
                    { 1, 1, "Ba Đình" },
                    { 2, 1, "Hoàn Kiếm" },
                    { 3, 1, "Hai Bà Trưng" },
                    { 4, 2, "Quận 1" },
                    { 5, 2, "Quận 2" },
                    { 6, 2, "Quận 3" },
                    { 7, 3, "Hồng Bàng" },
                    { 8, 3, "Ngô Quyền" },
                    { 9, 3, "Lê Chân" },
                    { 10, 4, "Hải Châu" },
                    { 11, 4, "Thanh Khê" },
                    { 12, 4, "Sơn Trà" },
                    { 13, 5, "Ninh Kiều" },
                    { 14, 5, "Bình Thủy" },
                    { 15, 5, "Cái Răng" },
                    { 16, 6, "Hà Giang" },
                    { 17, 6, "Đồng Văn" },
                    { 18, 6, "Mèo Vạc" },
                    { 19, 7, "Cao Bằng" },
                    { 20, 7, "Bảo Lâm" },
                    { 21, 7, "Hà Quảng" },
                    { 22, 8, "Lai Châu" },
                    { 23, 8, "Tam Đường" },
                    { 24, 8, "Mường Tè" },
                    { 25, 9, "Lào Cai" },
                    { 26, 9, "Bát Xát" },
                    { 27, 9, "Sa Pa" },
                    { 28, 10, "Lạng Sơn" },
                    { 29, 10, "Cao Lộc" },
                    { 30, 10, "Tràng Định" }
                });

            migrationBuilder.InsertData(
                table: "Wards",
                columns: new[] { "WardId", "DistrictId", "WardName" },
                values: new object[,]
                {
                    { 1, 2, "Tràng Tiền" },
                    { 2, 2, "Lý Thái Tổ" },
                    { 3, 1, "Trúc Bạch" },
                    { 4, 1, "Ngọc Hà" },
                    { 5, 3, "Bách Khoa" },
                    { 6, 3, "Thượng Đình" },
                    { 7, 4, "Bến Nghé" },
                    { 8, 4, "Cầu Ông Lãnh" },
                    { 9, 5, "Thảo Điền" },
                    { 10, 5, "An Phú" },
                    { 11, 6, "Nguyễn Thái Bình" },
                    { 12, 6, "Phạm Ngũ Lão" },
                    { 13, 7, "Cầu Đất" },
                    { 14, 7, "Lê Lợi" },
                    { 15, 8, "Quán Bàu" },
                    { 16, 8, "Lạch Tray" },
                    { 17, 9, "Cầu Tre" },
                    { 18, 9, "Nghĩa Xá" },
                    { 19, 10, "Thạch Thang" },
                    { 20, 10, "Hòa Cường Bắc" },
                    { 21, 11, "Thanh Bình" },
                    { 22, 11, "An Khê" },
                    { 23, 12, "Mân Thái" },
                    { 24, 12, "An Hải Đông" },
                    { 25, 13, "An Bình" },
                    { 26, 13, "An Cư" },
                    { 27, 14, "Cái Khế" },
                    { 28, 14, "Thới Bình" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_EmployeeId",
                table: "Certificates",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                table: "Districts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CityId",
                table: "Employees",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EthnicId",
                table: "Employees",
                column: "EthnicId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobId",
                table: "Employees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WardId",
                table: "Employees",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictId",
                table: "Wards",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Ethnics");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
