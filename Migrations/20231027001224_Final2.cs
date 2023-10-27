using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class Final2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskFormDb",
                columns: table => new
                {
                    TaskFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskFormName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskFormDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskFormData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskFormPersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskModelFormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskFormDb", x => x.TaskFormId);
                    table.ForeignKey(
                        name: "FK_TaskFormDb_TaskDb_TaskModelFormId",
                        column: x => x.TaskModelFormId,
                        principalTable: "TaskDb",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMessengerDb",
                columns: table => new
                {
                    MessengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Messenge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskModelid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMessengerDb", x => x.MessengerId);
                    table.ForeignKey(
                        name: "FK_TaskMessengerDb_TaskDb_TaskModelid",
                        column: x => x.TaskModelid,
                        principalTable: "TaskDb",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskFormDb_TaskModelFormId",
                table: "TaskFormDb",
                column: "TaskModelFormId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMessengerDb_TaskModelid",
                table: "TaskMessengerDb",
                column: "TaskModelid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskFormDb");

            migrationBuilder.DropTable(
                name: "TaskMessengerDb");
        }
    }
}
