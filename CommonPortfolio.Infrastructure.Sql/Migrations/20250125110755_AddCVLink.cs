using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonPortfolio.Infrastructure.Sql.Migrations
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
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CVLink",
                table: "AccountDetails",
                type: "nvarchar(250)",
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
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
