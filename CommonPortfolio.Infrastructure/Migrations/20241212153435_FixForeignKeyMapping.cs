using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountDetails_AppUsers_UserId1",
                table: "AccountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountLinks_AppUsers_UserId1",
                table: "AccountLinks");

            migrationBuilder.DropIndex(
                name: "IX_AccountLinks_UserId1",
                table: "AccountLinks");

            migrationBuilder.DropIndex(
                name: "IX_AccountDetails_UserId1",
                table: "AccountDetails");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AccountLinks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AccountDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "AccountLinks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "AccountDetails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AccountLinks_UserId1",
                table: "AccountLinks",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetails_UserId1",
                table: "AccountDetails",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountDetails_AppUsers_UserId1",
                table: "AccountDetails",
                column: "UserId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountLinks_AppUsers_UserId1",
                table: "AccountLinks",
                column: "UserId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
