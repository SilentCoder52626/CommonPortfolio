using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonPortfolio.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class RefacAccontDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerPicturePublicId",
                table: "AccountDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePublicId",
                table: "AccountDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerPicturePublicId",
                table: "AccountDetails");

            migrationBuilder.DropColumn(
                name: "ProfilePicturePublicId",
                table: "AccountDetails");
        }
    }
}
