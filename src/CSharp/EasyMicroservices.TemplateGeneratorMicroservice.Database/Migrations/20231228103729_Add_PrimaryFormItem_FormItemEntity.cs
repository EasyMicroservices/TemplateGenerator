using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_PrimaryFormItem_FormItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrimaryFormItemId",
                table: "FormItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_PrimaryFormItemId",
                table: "FormItems",
                column: "PrimaryFormItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItems_FormItems_PrimaryFormItemId",
                table: "FormItems",
                column: "PrimaryFormItemId",
                principalTable: "FormItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItems_FormItems_PrimaryFormItemId",
                table: "FormItems");

            migrationBuilder.DropIndex(
                name: "IX_FormItems_PrimaryFormItemId",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "PrimaryFormItemId",
                table: "FormItems");
        }
    }
}
