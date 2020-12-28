using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksManagementApp.Migrations
{
    public partial class ChangePasswordHashAndSaltTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(172) CHARACTER SET utf8mb4",
                oldFixedLength: true,
                oldMaxLength: 172);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(88) CHARACTER SET utf8mb4",
                oldFixedLength: true,
                oldMaxLength: 88);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 175, 251, 64, 208, 147, 177, 29, 202, 108, 224, 181, 107, 156, 178, 34, 40, 211, 224, 229, 254, 145, 165, 220, 153, 2, 2, 127, 179, 74, 83, 79, 126, 169, 6, 4, 46, 164, 202, 81, 214, 174, 68, 42, 57, 34, 154, 24, 249, 45, 8, 239, 243, 47, 235, 36, 223, 88, 142, 1, 9, 6, 45, 238, 64 }, new byte[] { 61, 163, 157, 192, 193, 105, 41, 47, 230, 118, 231, 133, 9, 35, 54, 229, 17, 180, 137, 144, 158, 225, 167, 6, 222, 127, 206, 31, 41, 71, 1, 165, 218, 65, 112, 197, 19, 68, 167, 184, 35, 75, 124, 125, 75, 13, 163, 67, 207, 94, 217, 113, 215, 238, 137, 3, 65, 126, 129, 172, 239, 54, 98, 18, 146, 238, 26, 100, 77, 159, 62, 102, 229, 133, 43, 110, 225, 242, 8, 201, 43, 241, 254, 81, 58, 143, 160, 218, 161, 215, 124, 1, 158, 110, 164, 192, 176, 152, 160, 214, 215, 9, 173, 0, 178, 53, 228, 232, 93, 23, 40, 146, 154, 127, 152, 30, 243, 92, 40, 19, 243, 238, 67, 192, 40, 111, 89, 14 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 199, 214, 32, 68, 41, 156, 27, 230, 238, 187, 125, 211, 68, 32, 211, 85, 89, 37, 193, 116, 163, 91, 38, 152, 208, 162, 240, 246, 134, 167, 94, 205, 234, 236, 134, 210, 47, 255, 157, 56, 231, 0, 134, 105, 20, 216, 153, 147, 55, 7, 70, 197, 229, 22, 83, 69, 97, 59, 69, 50, 58, 114, 61, 157 }, new byte[] { 109, 180, 179, 238, 14, 93, 212, 249, 250, 155, 69, 255, 202, 12, 77, 163, 59, 141, 73, 40, 179, 110, 163, 66, 147, 242, 202, 191, 226, 112, 64, 70, 121, 7, 129, 35, 209, 77, 58, 17, 95, 192, 124, 0, 196, 185, 25, 235, 253, 251, 232, 60, 97, 70, 221, 94, 109, 240, 0, 191, 181, 97, 167, 38, 44, 234, 153, 144, 52, 178, 30, 46, 245, 140, 231, 164, 103, 180, 29, 241, 235, 246, 158, 216, 234, 158, 251, 197, 255, 77, 140, 213, 233, 128, 13, 20, 183, 254, 81, 228, 72, 246, 148, 43, 11, 49, 201, 105, 153, 110, 127, 29, 163, 37, 33, 128, 138, 78, 115, 237, 154, 250, 26, 232, 75, 2, 126, 182 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "char(172) CHARACTER SET utf8mb4",
                fixedLength: true,
                maxLength: 172,
                nullable: false,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "char(88) CHARACTER SET utf8mb4",
                fixedLength: true,
                maxLength: 88,
                nullable: false,
                oldClrType: typeof(byte[]));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "gLWp0wheQxpUGPaskVRzPZNxOCKrrtyCUhCAc6NXMDoAq/Ue5R3xLBlcndd0KljfixuYOX44alAlgG0gesx6QQ==", "Pg8hoAZPebOtXFJZmabdIaAlNpOrPBodPRiM12pDarEixuC062HqQ06nSJEuYKashnD5vGeoxzuhIJvHsSIJjCJ5FpydgYEiW209QvziQvhVykve6efUJZPg/uzHm+HKqdxOxIE2iIlJMLzcTbJxrMO0pOc+rrzvsvlJYXCklpQ=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "jSMhlXJhO4uOGhb6jftBnyIgn9FhF8dKR0OC4BjqLt7rshEhSebGHsENXsqwYmD5F7aHvX6h94f1iVlB8B5lhA==", "tqpH4P1AMVug1AVm+2ZFaLcSuJ1Hvj+7udjq6KDEMvC5Rpd/2d0fYi2fIuBZTNSS+mUFWuldsDH04U31HB+WqWQpIr0Fv8xNk9ecYR66RtiMBA9zL7deLOfAOWTpEN1VHu4kijABDdYt9M0iUkoFFfapSEcTmDSUwo6HR2ZadJA=" });
        }
    }
}
