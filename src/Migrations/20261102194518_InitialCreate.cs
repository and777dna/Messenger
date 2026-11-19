using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Messenger.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsPrivate = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    ChatsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MembersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.ChatsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_ChatUser_Chats_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdempotencyKey = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SendDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChatId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SenderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "IsPrivate" },
                values: new object[,]
                {
                    { new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d0001"), false },
                    { new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d0002"), false }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "RegistrationDate" },
                values: new object[] { new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d1001"), "seed-user@example.com", "seed-user", "seed-password-hash", new DateOnly(2026, 1, 1) });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "Content", "IdempotencyKey", "IsRead", "SendDate", "SenderId", "UserId" },
                values: new object[,]
                {
                    { new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2001"), new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d0001"), "Hello!", null, true, new DateOnly(2026, 1, 1), new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d1001"), null },
                    { new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2002"), new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d0001"), "How are you?", null, true, new DateOnly(2026, 1, 2), new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d1001"), null },
                    { new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2003"), new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d0001"), "This is the latest seeded message.", null, false, new DateOnly(2026, 1, 3), new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d1001"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_MembersId",
                table: "ChatUser",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Id_SendDate",
                table: "Messages",
                columns: new[] { "Id", "SendDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IdempotencyKey",
                table: "Messages",
                column: "IdempotencyKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
