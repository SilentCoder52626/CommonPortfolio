using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCVLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WEB3FormsAcessKey",
                table: "Settings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "CVLink",
                table: "AccountDetails",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVLink",
                table: "AccountDetails");

            migrationBuilder.AlterColumn<string>(
                name: "WEB3FormsAcessKey",
                table: "Settings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
