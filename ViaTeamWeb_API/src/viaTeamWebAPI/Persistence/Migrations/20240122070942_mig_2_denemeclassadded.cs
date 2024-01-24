using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2_denemeclassadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Denemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denemes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Denemes.Admin", null },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Denemes.Read", null },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Denemes.Write", null },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Denemes.Create", null },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Denemes.Update", null },
                    { 26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Denemes.Delete", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 214, 7, 138, 156, 154, 110, 52, 163, 219, 140, 73, 75, 217, 155, 159, 235, 23, 220, 150, 228, 31, 243, 28, 234, 188, 94, 159, 161, 117, 167, 232, 98, 207, 88, 223, 191, 152, 192, 192, 26, 75, 78, 250, 8, 181, 51, 44, 250, 243, 188, 61, 141, 145, 177, 242, 62, 32, 119, 118, 47, 37, 82, 129, 17 }, new byte[] { 162, 35, 73, 61, 82, 248, 18, 76, 199, 44, 200, 103, 197, 183, 231, 23, 240, 124, 247, 223, 97, 244, 86, 168, 7, 108, 164, 209, 221, 141, 76, 165, 201, 171, 155, 131, 205, 88, 203, 252, 71, 96, 191, 245, 3, 8, 199, 4, 85, 199, 184, 241, 80, 57, 148, 74, 207, 178, 255, 188, 173, 248, 248, 30, 6, 145, 206, 122, 206, 118, 38, 49, 251, 183, 83, 133, 150, 253, 165, 1, 216, 25, 131, 197, 80, 69, 181, 33, 2, 212, 21, 0, 5, 134, 138, 57, 87, 86, 160, 21, 236, 50, 201, 52, 193, 101, 91, 41, 66, 140, 170, 87, 28, 42, 49, 191, 24, 42, 182, 147, 164, 253, 64, 36, 43, 205, 96, 62 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Denemes");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 43, 90, 182, 131, 76, 247, 72, 88, 69, 72, 79, 11, 142, 248, 87, 69, 7, 183, 169, 17, 186, 125, 79, 85, 80, 31, 198, 128, 28, 204, 193, 208, 178, 50, 75, 82, 2, 101, 0, 171, 75, 22, 47, 144, 188, 230, 68, 227, 55, 90, 179, 82, 101, 202, 22, 238, 183, 56, 16, 92, 95, 4, 110, 80 }, new byte[] { 210, 222, 64, 175, 11, 154, 134, 222, 242, 52, 69, 40, 240, 142, 66, 8, 3, 31, 216, 148, 69, 31, 150, 183, 206, 82, 227, 47, 167, 164, 246, 135, 206, 167, 48, 177, 70, 201, 232, 73, 108, 232, 154, 41, 37, 46, 111, 103, 236, 13, 107, 14, 84, 37, 85, 35, 92, 71, 111, 144, 62, 198, 87, 14, 123, 201, 92, 115, 228, 220, 133, 137, 200, 40, 133, 134, 190, 6, 31, 248, 150, 112, 208, 248, 119, 85, 2, 202, 62, 133, 245, 122, 16, 161, 167, 230, 26, 189, 74, 147, 164, 91, 73, 70, 210, 83, 144, 127, 45, 245, 234, 206, 226, 80, 110, 91, 146, 131, 44, 97, 129, 254, 15, 142, 65, 209, 198, 74 } });
        }
    }
}
