using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Make_FormItemEventId_Nullable_FormItemEventActionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEventActions_FormItemEvents_FormItemEventId",
                table: "FormItemEventActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents");

            migrationBuilder.AlterColumn<long>(
                name: "FormItemId",
                table: "FormItemEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FormItemEventId",
                table: "FormItemEventActions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "CreationDateTime", "DeletedDateTime", "Description", "IsDeleted", "ModificationDateTime", "Title", "Type", "UniqueIdentity" },
                values: new object[,]
                {
                    { 12L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "Column", (byte)17, null },
                    { 13L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "Menu", (byte)18, null },
                    { 14L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "Card", (byte)19, null },
                    { 15L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "Button", (byte)20, null },
                    { 16L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "HorizontalStack", (byte)21, null },
                    { 17L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "VerticalStack", (byte)22, null },
                    { 18L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "VerticalStack", (byte)23, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEventActions_FormItemEvents_FormItemEventId",
                table: "FormItemEventActions",
                column: "FormItemEventId",
                principalTable: "FormItemEvents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents",
                column: "FormItemId",
                principalTable: "FormItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEventActions_FormItemEvents_FormItemEventId",
                table: "FormItemEventActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents");

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.AlterColumn<long>(
                name: "FormItemId",
                table: "FormItemEvents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FormItemEventId",
                table: "FormItemEventActions",
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

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEventActions_FormItemEvents_FormItemEventId",
                table: "FormItemEventActions",
                column: "FormItemEventId",
                principalTable: "FormItemEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemEvents_FormItems_FormItemId",
                table: "FormItemEvents",
                column: "FormItemId",
                principalTable: "FormItems",
                principalColumn: "Id");
        }
    }
}
