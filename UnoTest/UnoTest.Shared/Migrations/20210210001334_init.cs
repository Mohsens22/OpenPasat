using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnoTest.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    Education = table.Column<int>(nullable: false),
                    MaritalStatus = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    ClinicalHistory = table.Column<string>(nullable: true),
                    DrugAbuseHistory = table.Column<string>(nullable: true),
                    OtherInfo = table.Column<string>(nullable: true),
                    YearBorn = table.Column<DateTimeOffset>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestCount = table.Column<int>(nullable: false),
                    Quantum = table.Column<int>(nullable: false),
                    ImpulseRate = table.Column<int>(nullable: false),
                    RepresentationType = table.Column<int>(nullable: false),
                    Correction = table.Column<bool>(nullable: false),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fragments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(nullable: false),
                    PreviousAnswer = table.Column<int>(nullable: true),
                    CloseAnswers = table.Column<string>(nullable: true),
                    RepresentationTime = table.Column<DateTimeOffset>(nullable: false),
                    IndentifierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fragments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fragments_Tests_IndentifierId",
                        column: x => x.IndentifierId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Input = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    InputTime = table.Column<DateTimeOffset>(nullable: false),
                    InputSpeed = table.Column<long>(nullable: true),
                    InputType = table.Column<int>(nullable: false),
                    TestFragmentId = table.Column<int>(nullable: true),
                    PreFragmentId = table.Column<int>(nullable: true),
                    IndentifierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Tests_IndentifierId",
                        column: x => x.IndentifierId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Fragments_PreFragmentId",
                        column: x => x.PreFragmentId,
                        principalTable: "Fragments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Fragments_TestFragmentId",
                        column: x => x.TestFragmentId,
                        principalTable: "Fragments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClinicalHistory", "CreatedAt", "DrugAbuseHistory", "Education", "FullName", "Gender", "Job", "MaritalStatus", "OtherInfo", "Username", "YearBorn" },
                values: new object[] { 1, null, new DateTimeOffset(new DateTime(2021, 2, 10, 0, 13, 33, 622, DateTimeKind.Unspecified).AddTicks(8717), new TimeSpan(0, 0, 0, 0, 0)), null, 0, "Public", 0, null, 0, null, "public", null });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_IndentifierId",
                table: "Answers",
                column: "IndentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PreFragmentId",
                table: "Answers",
                column: "PreFragmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestFragmentId",
                table: "Answers",
                column: "TestFragmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_IndentifierId",
                table: "Fragments",
                column: "IndentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_UserId",
                table: "Tests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Fragments");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
