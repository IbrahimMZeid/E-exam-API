using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_exam.Migrations
{
    /// <inheritdoc />
    public partial class addexamproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "DurationInMinites",
               table: "Exams",
               type: "int",
               nullable: false,
               defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionsCount",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinites",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "QuestionsCount",
                table: "Exams");

        }
    }
}
