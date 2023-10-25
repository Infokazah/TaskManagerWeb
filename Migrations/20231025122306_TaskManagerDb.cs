using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class TaskManagerDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonDb",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonMailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDb", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDb",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDb", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "KategoriDb",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Projectid = table.Column<int>(type: "int", nullable: false),
                    KategoriProjectProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriDb", x => x.KategoriId);
                    table.ForeignKey(
                        name: "FK_KategoriDb_ProjectDb_KategoriProjectProjectId",
                        column: x => x.KategoriProjectProjectId,
                        principalTable: "ProjectDb",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonModelProjectModel",
                columns: table => new
                {
                    PersonProjectsProjectId = table.Column<int>(type: "int", nullable: false),
                    ProgectPersonPersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonModelProjectModel", x => new { x.PersonProjectsProjectId, x.ProgectPersonPersonId });
                    table.ForeignKey(
                        name: "FK_PersonModelProjectModel_PersonDb_ProgectPersonPersonId",
                        column: x => x.ProgectPersonPersonId,
                        principalTable: "PersonDb",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonModelProjectModel_ProjectDb_PersonProjectsProjectId",
                        column: x => x.PersonProjectsProjectId,
                        principalTable: "ProjectDb",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskDb",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDeadline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskImportance = table.Column<int>(type: "int", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    TackKategoriKategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDb", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskDb_KategoriDb_TackKategoriKategoriId",
                        column: x => x.TackKategoriKategoriId,
                        principalTable: "KategoriDb",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KategoriDb_KategoriProjectProjectId",
                table: "KategoriDb",
                column: "KategoriProjectProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonModelProjectModel_ProgectPersonPersonId",
                table: "PersonModelProjectModel",
                column: "ProgectPersonPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDb_TackKategoriKategoriId",
                table: "TaskDb",
                column: "TackKategoriKategoriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonModelProjectModel");

            migrationBuilder.DropTable(
                name: "TaskDb");

            migrationBuilder.DropTable(
                name: "PersonDb");

            migrationBuilder.DropTable(
                name: "KategoriDb");

            migrationBuilder.DropTable(
                name: "ProjectDb");
        }
    }
}
