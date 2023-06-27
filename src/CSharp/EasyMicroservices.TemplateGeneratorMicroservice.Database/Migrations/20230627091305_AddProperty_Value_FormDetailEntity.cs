using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyValueFormDetailEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "FormDetails");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "FormDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FormDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "FormDetails");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "FormDetails");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "FormDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
