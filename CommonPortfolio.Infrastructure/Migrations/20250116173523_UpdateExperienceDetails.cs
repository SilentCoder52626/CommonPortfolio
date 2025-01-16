using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExperienceDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienceDetails");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Experiences",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Experiences");

            migrationBuilder.CreateTable(
                name: "ExperienceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceDetails_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceDetails_ExperienceId",
                table: "ExperienceDetails",
                column: "ExperienceId");
        }
    }
}
