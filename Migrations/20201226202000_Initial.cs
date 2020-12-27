using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Role = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(fixedLength: true, maxLength: 88, nullable: false),
                    PasswordSalt = table.Column<string>(fixedLength: true, maxLength: 172, nullable: false),
                    ResetPasswordToken = table.Column<Guid>(nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    ResetPasswordTokenExpires = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                columns: new[] { "UserId", "Email", "Name", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { 1, "baranauskas.aidas@gmail.com", "Baranauskas Aidas", "gLWp0wheQxpUGPaskVRzPZNxOCKrrtyCUhCAc6NXMDoAq/Ue5R3xLBlcndd0KljfixuYOX44alAlgG0gesx6QQ==", "Pg8hoAZPebOtXFJZmabdIaAlNpOrPBodPRiM12pDarEixuC062HqQ06nSJEuYKashnD5vGeoxzuhIJvHsSIJjCJ5FpydgYEiW209QvziQvhVykve6efUJZPg/uzHm+HKqdxOxIE2iIlJMLzcTbJxrMO0pOc+rrzvsvlJYXCklpQ=", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { 2, "aidas.baranauskas@yahoo.com", "Aidas Baranauskas", "jSMhlXJhO4uOGhb6jftBnyIgn9FhF8dKR0OC4BjqLt7rshEhSebGHsENXsqwYmD5F7aHvX6h94f1iVlB8B5lhA==", "tqpH4P1AMVug1AVm+2ZFaLcSuJ1Hvj+7udjq6KDEMvC5Rpd/2d0fYi2fIuBZTNSS+mUFWuldsDH04U31HB+WqWQpIr0Fv8xNk9ecYR66RtiMBA9zL7deLOfAOWTpEN1VHu4kijABDdYt9M0iUkoFFfapSEcTmDSUwo6HR2ZadJA=", "user" });

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
