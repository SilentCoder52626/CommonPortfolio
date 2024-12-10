using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserInSkillType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SkillType",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SkillType_UserId",
                table: "SkillType",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillType_AppUsers_UserId",
                table: "SkillType",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillType_AppUsers_UserId",
                table: "SkillType");

            migrationBuilder.DropIndex(
                name: "IX_SkillType_UserId",
                table: "SkillType");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SkillType");
        }
    }
}
