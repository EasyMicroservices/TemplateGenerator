using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataItemTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Description", "Title", "Type" },
                values: new object[,]
                {
                    { 1L, null, "DateTime", (byte)10 },
                    { 2L, null, "DateOnly", (byte)11 },
                    { 3L, null, "TimeOnly", (byte)12 },
                    { 4L, null, "Label", (byte)13 },
                    { 5L, null, "CheckBox", (byte)8 },
                    { 6L, null, "CheckList", (byte)7 },
                    { 7L, null, "OptionList", (byte)9 },
                    { 8L, null, "TextBox", (byte)6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 8L);
        }
    }
}
