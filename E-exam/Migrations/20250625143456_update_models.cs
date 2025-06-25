using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_exam.Migrations
{
    /// <inheritdoc />
    public partial class update_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Exams_ExamId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ExamId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsTrueOrFalse",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "AnswerText",
                table: "StudentAnswers",
                newName: "SelectedOptionId");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "Questions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "Questions",
                newName: "SubjectId");

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_SelectedOptionId",
                table: "StudentAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SubjectId",
                table: "Questions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_ExamId",
                table: "ExamQuestion",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_QuestionId",
                table: "ExamQuestion",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Subjects_SubjectId",
                table: "Questions",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Options_SelectedOptionId",
                table: "StudentAnswers",
                column: "SelectedOptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Subjects_SubjectId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Options_SelectedOptionId",
                table: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_SelectedOptionId",
                table: "StudentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SubjectId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "SelectedOptionId",
                table: "StudentAnswers",
                newName: "AnswerText");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Questions",
                newName: "ExamId");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Questions",
                newName: "Answer");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrueOrFalse",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamId",
                table: "Questions",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Exams_ExamId",
                table: "Questions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
