using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIdentityToSomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "FormItemValues",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "FormFills",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemValues_UniqueIdentity",
                table: "FormItemValues",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormFills_UniqueIdentity",
                table: "FormFills",
                column: "UniqueIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FormItemValues_UniqueIdentity",
                table: "FormItemValues");

            migrationBuilder.DropIndex(
                name: "IX_FormFills_UniqueIdentity",
                table: "FormFills");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "FormItemValues");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "FormFills");
        }
    }
}
