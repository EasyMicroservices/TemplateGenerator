using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_FormItemId_FormItemEventActionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents");

            migrationBuilder.AlterColumn<long>(
                name: "FormItemId",
                table: "FormItemEvents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "FormItemId",
                table: "FormItemEventActions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "FormItemEventActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_FormItemId",
                table: "FormItemEventActions",
                column: "FormItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEventActions_FormItems_FormItemId",
                table: "FormItemEventActions",
                column: "FormItemId",
                principalTable: "FormItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents",
                column: "FormItemId",
                principalTable: "FormItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEventActions_FormItems_FormItemId",
                table: "FormItemEventActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents");

            migrationBuilder.DropIndex(
                name: "IX_FormItemEventActions_FormItemId",
                table: "FormItemEventActions");

            migrationBuilder.DropColumn(
                name: "FormItemId",
                table: "FormItemEventActions");

            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "FormItemEventActions");

            migrationBuilder.AlterColumn<long>(
                name: "FormItemId",
                table: "FormItemEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2738));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2739));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2741));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2742));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2743));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2704));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2719));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDateTime",
                value: new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2721));

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents",
                column: "FormItemId",
                principalTable: "FormItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
