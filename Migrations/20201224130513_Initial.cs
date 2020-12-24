using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TasksManagementApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Role = table.Column<string>(maxLength: 10, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash", "Role" },
                values: new object[] { 1, "baranauskas.aidas@gmail.com", "$2a$11$CzcOaxOxuZHcMO9tT5yueua9NZKe84.cNdUarlxq0LBh83B5Bb0zq", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash", "Role" },
                values: new object[] { 2, "aidas.baranauskas@yahoo.com", "$2a$11$BhOFCvC6eHYPc9CSlUehmODG4WhPJTjVxqXJ/iAS3WYwkYZIilSNy", "user" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "IsCompleted", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, false, "Manage team tasks", 1 },
                    { 2, true, "Track progress", 1 },
                    { 3, false, "Buy product", 2 },
                    { 4, false, "Sell product", 2 },
                    { 5, false, "Manage transportation", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
