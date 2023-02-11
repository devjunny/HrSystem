using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrSystem.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    job_title = table.Column<string>(nullable: true),
                    min_salary = table.Column<double>(nullable: false),
                    max_salary = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    manager_firstName = table.Column<string>(nullable: true),
                    manager_lastName = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    department_name = table.Column<string>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "job_title", "max_salary", "min_salary" },
                values: new object[,]
                {
                    { new Guid("02a73fcc-7bdf-4f47-99e5-3a00cbbd69d4"), "Software Engineering", 5000.0, 1500.0 },
                    { new Guid("dbc5233f-d894-43cb-a3e3-e6b842a67feb"), "Accountant", 4000.0, 1500.0 }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "DepartmentId", "address", "email", "manager_firstName", "manager_lastName", "phone" },
                values: new object[,]
                {
                    { new Guid("bc528192-2726-4f85-b7f1-cc278a87052e"), null, "Kofi Shito Street 45", "rodgers@live.com", "Kenny", "Rodgers", "0243784512" },
                    { new Guid("d36acd2e-1506-4362-9e16-6772008883b8"), null, "Olusegu Obasanjo 12T", "dolly@live.com", "Dolly", "Parton", "0206857945" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "ManagerId", "department_name" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("bc528192-2726-4f85-b7f1-cc278a87052e"), "Finance" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "ManagerId", "department_name" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new Guid("d36acd2e-1506-4362-9e16-6772008883b8"), "Accounting" });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_DepartmentId",
                table: "Managers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_DepartmentId",
                table: "Managers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Managers_ManagerId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
