using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexPropertyFormItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "FormItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "FormDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "FormDetails");
        }
    }
}
