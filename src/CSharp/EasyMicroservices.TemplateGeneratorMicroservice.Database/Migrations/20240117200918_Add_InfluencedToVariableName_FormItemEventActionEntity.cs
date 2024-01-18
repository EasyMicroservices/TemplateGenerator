using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_InfluencedToVariableName_FormItemEventActionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfluencedToVariableName",
                table: "FormItemEventActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8630));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8634));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8635));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8637));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8638));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8576));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8603));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 23, 39, 17, 974, DateTimeKind.Local).AddTicks(8604));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfluencedToVariableName",
                table: "FormItemEventActions");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4586));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4587));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4589));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4590));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4591));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4543));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4558));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 17, 13, 39, 5, 484, DateTimeKind.Local).AddTicks(4559));
        }
    }
}
