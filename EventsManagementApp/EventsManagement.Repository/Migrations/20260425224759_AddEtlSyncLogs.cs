using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEtlSyncLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Row",
                table: "Seats",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<Guid>(
                name: "EventImageId",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EtlSyncLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JobName = table.Column<string>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    ErrorMessage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtlSyncLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventsImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventImageId",
                table: "Events",
                column: "EventImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventsImages_EventImageId",
                table: "Events",
                column: "EventImageId",
                principalTable: "EventsImages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventsImages_EventImageId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EtlSyncLogs");

            migrationBuilder.DropTable(
                name: "EventsImages");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventImageId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventImageId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "Row",
                table: "Seats",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
