using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_ParentId_FormItemEventActionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FormItemEventActionEntityId",
                table: "FormItemEventActions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "FormItemEventActions",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5161));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5176));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 10, 42, 7, 281, DateTimeKind.Local).AddTicks(5177));

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_FormItemEventActionEntityId",
                table: "FormItemEventActions",
                column: "FormItemEventActionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEventActions_FormItemEventActions_FormItemEventActionEntityId",
                table: "FormItemEventActions",
                column: "FormItemEventActionEntityId",
                principalTable: "FormItemEventActions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEventActions_FormItemEventActions_FormItemEventActionEntityId",
                table: "FormItemEventActions");

            migrationBuilder.DropIndex(
                name: "IX_FormItemEventActions_FormItemEventActionEntityId",
                table: "FormItemEventActions");

            migrationBuilder.DropColumn(
                name: "FormItemEventActionEntityId",
                table: "FormItemEventActions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "FormItemEventActions");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1924));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1925));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1927));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1895));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 8, 9, 59, 30, 445, DateTimeKind.Local).AddTicks(1906));
        }
    }
}
