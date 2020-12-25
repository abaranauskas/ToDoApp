using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksManagementApp.Migrations
{
    public partial class MapUserNameProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { "Baranauskas Aidas", "w4SetS8UIwayc3tTTJQ/ep8klcIGW1UL/CiazbUQs1I0iZ4xTrHXPo41cDmnyhmG3FdJ7ElXZGYXta6c9DjnCg==", "kj6rg6man7HE4xrLKhzxfcLmnv8ctXsapEhnszNcerDrV91KPNUl8OUQbtd8BI8kU9XZxh12MHBmhhCaeGnvPUNCgKHJKQNJnLd1O/PEZP4Lc6VBAzkTjSMMGpVVjHDRMOu3cqpcr7V61Pb4x0kpAbZR/LvoS4eHR1Az+KNaByk=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { "Aidas Baranauskas", "4pFITJ87EAYTnHUtTrv3VEt3e8pSQzgv97VXvL4GuSPFIgpZJb+5oqnp74lxXny8CJktOY1sY0GoPxWQ825RUw==", "YHu3Ymd79/8SaXjY8TZ1U/vtYeNW+TNrHpRfO1E2LFDt0lC4zcku7j3MuzHr4X2+P0/BkL9A4V2O68iRBkx6xsovfemVnFZoJtqpSNUXQlLPWyOYtXEkSY0lQq4Th5Lj/UZtpMpPOlMlLLeLGgGQ7mgazcsXGWPD4MX+iy233Go=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

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
    }
}
