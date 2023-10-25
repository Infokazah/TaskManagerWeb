using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KategoriDb_ProjectDb_KategoriProjectProjectId",
                table: "KategoriDb");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskDb_KategoriDb_TackKategoriKategoriId",
                table: "TaskDb");

            migrationBuilder.DropIndex(
                name: "IX_TaskDb_TackKategoriKategoriId",
                table: "TaskDb");

            migrationBuilder.DropIndex(
                name: "IX_KategoriDb_KategoriProjectProjectId",
                table: "KategoriDb");

            migrationBuilder.DropColumn(
                name: "TackKategoriKategoriId",
                table: "TaskDb");

            migrationBuilder.DropColumn(
                name: "KategoriProjectProjectId",
                table: "KategoriDb");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDb_KategoriId",
                table: "TaskDb",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_KategoriDb_Projectid",
                table: "KategoriDb",
                column: "Projectid");

            migrationBuilder.AddForeignKey(
                name: "FK_KategoriDb_ProjectDb_Projectid",
                table: "KategoriDb",
                column: "Projectid",
                principalTable: "ProjectDb",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDb_KategoriDb_KategoriId",
                table: "TaskDb",
                column: "KategoriId",
                principalTable: "KategoriDb",
                principalColumn: "KategoriId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KategoriDb_ProjectDb_Projectid",
                table: "KategoriDb");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskDb_KategoriDb_KategoriId",
                table: "TaskDb");

            migrationBuilder.DropIndex(
                name: "IX_TaskDb_KategoriId",
                table: "TaskDb");

            migrationBuilder.DropIndex(
                name: "IX_KategoriDb_Projectid",
                table: "KategoriDb");

            migrationBuilder.AddColumn<int>(
                name: "TackKategoriKategoriId",
                table: "TaskDb",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KategoriProjectProjectId",
                table: "KategoriDb",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskDb_TackKategoriKategoriId",
                table: "TaskDb",
                column: "TackKategoriKategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_KategoriDb_KategoriProjectProjectId",
                table: "KategoriDb",
                column: "KategoriProjectProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_KategoriDb_ProjectDb_KategoriProjectProjectId",
                table: "KategoriDb",
                column: "KategoriProjectProjectId",
                principalTable: "ProjectDb",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDb_KategoriDb_TackKategoriKategoriId",
                table: "TaskDb",
                column: "TackKategoriKategoriId",
                principalTable: "KategoriDb",
                principalColumn: "KategoriId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
