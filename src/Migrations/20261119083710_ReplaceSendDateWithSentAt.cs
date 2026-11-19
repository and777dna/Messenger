using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messenger.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceSendDateWithSentAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SentAt",
                table: "Messages",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            // Keep the send date of existing messages (the old column had no time, so it becomes midnight UTC).
            migrationBuilder.Sql("UPDATE `Messages` SET `SentAt` = `SendDate`;");

            // The new index must exist before IX_Messages_ChatId is dropped:
            // MySQL refuses to drop the only index that backs the ChatId foreign key.
            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId_SentAt",
                table: "Messages",
                columns: new[] { "ChatId", "SentAt" });

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Id_SendDate",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2001"),
                column: "SentAt",
                value: new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2002"),
                column: "SentAt",
                value: new DateTimeOffset(new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2003"),
                column: "SentAt",
                value: new DateTimeOffset(new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "SendDate",
                table: "Messages",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.Sql("UPDATE `Messages` SET `SendDate` = DATE(`SentAt`);");

            // Recreate the foreign key index first, for the same reason as in Up.
            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatId_SentAt",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Id_SendDate",
                table: "Messages",
                columns: new[] { "Id", "SendDate" });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2001"),
                column: "SendDate",
                value: new DateOnly(2026, 1, 1));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2002"),
                column: "SendDate",
                value: new DateOnly(2026, 1, 2));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2003"),
                column: "SendDate",
                value: new DateOnly(2026, 1, 3));
        }
    }
}
