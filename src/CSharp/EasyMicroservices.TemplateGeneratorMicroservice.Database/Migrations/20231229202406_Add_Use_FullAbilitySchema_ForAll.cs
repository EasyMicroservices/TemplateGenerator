using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_Use_FullAbilitySchema_ForAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "ItemTypes",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "FormItems",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "FormDetails",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "UniqueIdentity",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_UniqueIdentity",
                table: "ItemTypes",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_UniqueIdentity",
                table: "FormItems",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormDetails_UniqueIdentity",
                table: "FormDetails",
                column: "UniqueIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_UniqueIdentity",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_FormItems_UniqueIdentity",
                table: "FormItems");

            migrationBuilder.DropIndex(
                name: "IX_FormDetails_UniqueIdentity",
                table: "FormDetails");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "FormDetails");
        }
    }
}
