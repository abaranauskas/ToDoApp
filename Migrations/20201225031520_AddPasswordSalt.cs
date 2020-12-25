using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksManagementApp.Migrations
{
    public partial class AddPasswordSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                fixedLength: true,
                maxLength: 88,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                fixedLength: true,
                maxLength: 172,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "L90X7/zHi4tcoqOAM26ytTlBEjIRajbOhU7QBthtfhEGNxez/jCLB+x5l9r+3CE764arcCt66iYxNwucdkHgwg==", "P56Q8ZeG8tgr3nis5Xs/FJMj+YoFmZLZkVw7pvMJKG0s9SdYGQGanFftIks4DxKXZK50RF188MFBaX429p4FuwbVoleB3RtOvbnU9mqyCfLFEynarHE1R4AoWmLisSff1gnAy9gBB5KLTd3MiD+FwUNpB7dQbicMnO+bGyASlEg=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "3Sy4BZbEJ+U1UkZF4pWQcH9pxASJuMHuZXCQmZYjNIyHOshob99ZwgFe0LEe50igCh/tq1ghWX6N+t7ksobiOg==", "y70wnWyMJSP5L0Jc+/r0QtykcG285FE/XjIBX7pKr05cM/bFrU8dtIJnjt8oODSTUZJp6zQrhAPmIi6tpECvKDzsNxSW4/Qqo4Wq5GWtpY5e759uoPTMdM0YKxQLHdiJrduedbBl1KgoP38TWfd1SNx7cxfDKz3wJ5aDoCCObDg=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 88);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$CzcOaxOxuZHcMO9tT5yueua9NZKe84.cNdUarlxq0LBh83B5Bb0zq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$BhOFCvC6eHYPc9CSlUehmODG4WhPJTjVxqXJ/iAS3WYwkYZIilSNy");
        }
    }
}
