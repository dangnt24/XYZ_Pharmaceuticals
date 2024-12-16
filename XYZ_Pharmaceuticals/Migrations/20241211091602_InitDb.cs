using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XYZ_Pharmaceuticals.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Output = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapsuleSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineDimension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingWeight = table.Column<float>(type: "real", nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPressure = table.Column<float>(type: "real", nullable: true),
                    MaxDiameter = table.Column<float>(type: "real", nullable: true),
                    MaxDepth = table.Column<float>(type: "real", nullable: true),
                    ProductionCapacity = table.Column<int>(type: "int", nullable: true),
                    AirPressure = table.Column<float>(type: "real", nullable: true),
                    AirVolume = table.Column<float>(type: "real", nullable: true),
                    FillingSpeed = table.Column<int>(type: "int", nullable: true),
                    FillingRange = table.Column<float>(type: "real", nullable: true),
                    FillingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
					ID = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
					FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
					City = table.Column<string>(type: "nvarchar(max)", nullable: true),
					State = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
					AdminFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
			constraints: table =>
			{
				table.PrimaryKey("PK_QuoteUs", x => x.ID);
			});

			migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobApplications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CandidateId",
                table: "JobApplications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
