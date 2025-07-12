using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_exam.Migrations
{
    /// <inheritdoc />
    public partial class changeUserPkFromGuiToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                        name: "Users_Temp",
                        columns: table => new
                        {
                            Id = table.Column<int>(nullable: false)
                                .Annotation("SqlServer:Identity", "1, 1"),
                            Email = table.Column<string>(maxLength: 100, nullable: false),
                            PasswordHash = table.Column<string>(nullable: false),
                            Role = table.Column<string>(nullable: true)
                        },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_Users_Temp", x => x.Id);
                        });


            // 2. Drop the old table
            migrationBuilder.DropTable("Users");

            // 3. Rename the temp table
            migrationBuilder.RenameTable(
                name: "Users_Temp",
                newName: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Users_Old",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Email = table.Column<string>(maxLength: 100, nullable: false),
                PasswordHash = table.Column<string>(nullable: false),
                Role = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users_Old", x => x.Id);
            });


            migrationBuilder.DropTable("Users");
            migrationBuilder.RenameTable(
                name: "Users_Old",
                newName: "Users");
        }
    }
}

