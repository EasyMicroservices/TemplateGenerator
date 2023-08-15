using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_IDateTime_ISoftDelete_ToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "ItemTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "ItemTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ItemTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "ItemTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentity",
                table: "Forms",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Forms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Forms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Forms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "Forms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "FormItemValues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "FormItemValues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FormItemValues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "FormItemValues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "FormItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "FormItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FormItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "FormItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "FormFills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "FormFills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FormFills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "FormFills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "FormDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "FormDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FormDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "FormDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_CreationDateTime",
                table: "ItemTypes",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_DeletedDateTime",
                table: "ItemTypes",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_IsDeleted",
                table: "ItemTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_ModificationDateTime",
                table: "ItemTypes",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_CreationDateTime",
                table: "Forms",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_DeletedDateTime",
                table: "Forms",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_IsDeleted",
                table: "Forms",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_ModificationDateTime",
                table: "Forms",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_UniqueIdentity",
                table: "Forms",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemValues_CreationDateTime",
                table: "FormItemValues",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemValues_DeletedDateTime",
                table: "FormItemValues",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemValues_IsDeleted",
                table: "FormItemValues",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemValues_ModificationDateTime",
                table: "FormItemValues",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_CreationDateTime",
                table: "FormItems",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_DeletedDateTime",
                table: "FormItems",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_IsDeleted",
                table: "FormItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_ModificationDateTime",
                table: "FormItems",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormFills_CreationDateTime",
                table: "FormFills",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormFills_DeletedDateTime",
                table: "FormFills",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormFills_IsDeleted",
                table: "FormFills",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormFills_ModificationDateTime",
                table: "FormFills",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormDetails_CreationDateTime",
                table: "FormDetails",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormDetails_DeletedDateTime",
                table: "FormDetails",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormDetails_IsDeleted",
                table: "FormDetails",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormDetails_ModificationDateTime",
                table: "FormDetails",
                column: "ModificationDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_CreationDateTime",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_DeletedDateTime",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_IsDeleted",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_ModificationDateTime",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_Forms_CreationDateTime",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_DeletedDateTime",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_IsDeleted",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_ModificationDateTime",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_UniqueIdentity",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_FormItemValues_CreationDateTime",
                table: "FormItemValues");

            migrationBuilder.DropIndex(
                name: "IX_FormItemValues_DeletedDateTime",
                table: "FormItemValues");

            migrationBuilder.DropIndex(
                name: "IX_FormItemValues_IsDeleted",
                table: "FormItemValues");

            migrationBuilder.DropIndex(
                name: "IX_FormItemValues_ModificationDateTime",
                table: "FormItemValues");

            migrationBuilder.DropIndex(
                name: "IX_FormItems_CreationDateTime",
                table: "FormItems");

            migrationBuilder.DropIndex(
                name: "IX_FormItems_DeletedDateTime",
                table: "FormItems");

            migrationBuilder.DropIndex(
                name: "IX_FormItems_IsDeleted",
                table: "FormItems");

            migrationBuilder.DropIndex(
                name: "IX_FormItems_ModificationDateTime",
                table: "FormItems");

            migrationBuilder.DropIndex(
                name: "IX_FormFills_CreationDateTime",
                table: "FormFills");

            migrationBuilder.DropIndex(
                name: "IX_FormFills_DeletedDateTime",
                table: "FormFills");

            migrationBuilder.DropIndex(
                name: "IX_FormFills_IsDeleted",
                table: "FormFills");

            migrationBuilder.DropIndex(
                name: "IX_FormFills_ModificationDateTime",
                table: "FormFills");

            migrationBuilder.DropIndex(
                name: "IX_FormDetails_CreationDateTime",
                table: "FormDetails");

            migrationBuilder.DropIndex(
                name: "IX_FormDetails_DeletedDateTime",
                table: "FormDetails");

            migrationBuilder.DropIndex(
                name: "IX_FormDetails_IsDeleted",
                table: "FormDetails");

            migrationBuilder.DropIndex(
                name: "IX_FormDetails_ModificationDateTime",
                table: "FormDetails");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "FormItemValues");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "FormItemValues");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FormItemValues");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "FormItemValues");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "FormFills");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "FormFills");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FormFills");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "FormFills");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "FormDetails");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "FormDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FormDetails");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "FormDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentity",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true,
                oldCollation: "SQL_Latin1_General_CP1_CS_AS");
        }
    }
}
