using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    NationalCode = table.Column<string>(maxLength: 10, nullable: false),
                    PhoneNo = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.PersonnelId);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceTimes",
                columns: table => new
                {
                    AttendanceTimeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false),
                    TimeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceTimes", x => x.AttendanceTimeId);
                    table.ForeignKey(
                        name: "FK_AttendanceTimes_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false),
                    TimeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                    table.ForeignKey(
                        name: "FK_Days_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    UserRole = table.Column<int>(nullable: false),
                    UserActived = table.Column<bool>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSpans",
                columns: table => new
                {
                    AttendanceSpanId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeType = table.Column<int>(nullable: false),
                    StartAttendanceTimeId = table.Column<long>(nullable: true),
                    EndAttendanceTimeId = table.Column<long>(nullable: true),
                    DayId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSpans", x => x.AttendanceSpanId);
                    table.ForeignKey(
                        name: "FK_AttendanceSpans_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceSpans_AttendanceTimes_EndAttendanceTimeId",
                        column: x => x.EndAttendanceTimeId,
                        principalTable: "AttendanceTimes",
                        principalColumn: "AttendanceTimeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceSpans_AttendanceTimes_StartAttendanceTimeId",
                        column: x => x.StartAttendanceTimeId,
                        principalTable: "AttendanceTimes",
                        principalColumn: "AttendanceTimeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionRequests",
                columns: table => new
                {
                    ActionRequestId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    AttendanceSpanId = table.Column<long>(nullable: true),
                    DayId = table.Column<long>(nullable: true),
                    RequestType = table.Column<int>(nullable: false),
                    ApplicantPersonnelId = table.Column<int>(nullable: true),
                    CorroborantPersonnelId = table.Column<int>(nullable: true),
                    DescriptionOfApplicant = table.Column<string>(maxLength: 250, nullable: true),
                    DescriptionOfCorroborant = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRequests", x => x.ActionRequestId);
                    table.ForeignKey(
                        name: "FK_ActionRequests_Personnels_ApplicantPersonnelId",
                        column: x => x.ApplicantPersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionRequests_AttendanceSpans_AttendanceSpanId",
                        column: x => x.AttendanceSpanId,
                        principalTable: "AttendanceSpans",
                        principalColumn: "AttendanceSpanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionRequests_Personnels_CorroborantPersonnelId",
                        column: x => x.CorroborantPersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionRequests_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionRequests_ApplicantPersonnelId",
                table: "ActionRequests",
                column: "ApplicantPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRequests_AttendanceSpanId",
                table: "ActionRequests",
                column: "AttendanceSpanId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRequests_CorroborantPersonnelId",
                table: "ActionRequests",
                column: "CorroborantPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRequests_DayId",
                table: "ActionRequests",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSpans_DayId",
                table: "AttendanceSpans",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSpans_EndAttendanceTimeId",
                table: "AttendanceSpans",
                column: "EndAttendanceTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSpans_StartAttendanceTimeId",
                table: "AttendanceSpans",
                column: "StartAttendanceTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceTimes_PersonnelId",
                table: "AttendanceTimes",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_PersonnelId",
                table: "Days",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonnelId",
                table: "Users",
                column: "PersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AttendanceSpans");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "AttendanceTimes");

            migrationBuilder.DropTable(
                name: "Personnels");
        }
    }
}
