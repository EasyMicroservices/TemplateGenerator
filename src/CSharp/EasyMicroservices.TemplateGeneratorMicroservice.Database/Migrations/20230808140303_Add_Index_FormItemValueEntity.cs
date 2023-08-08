using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_Index_FormItemValueEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "FormItemValues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "FormItemValues");
        }
    }
}
