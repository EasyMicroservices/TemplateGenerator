using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyMicroservices.TemplateGeneratorMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Add_Actions_And_Events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalVariableName",
                table: "FormItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Actions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Actions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormItemActionJobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<long>(type: "bigint", nullable: false),
                    FormItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemActionJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormItemActionJobs_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormItemActionJobs_FormItems_FormItemId",
                        column: x => x.FormItemId,
                        principalTable: "FormItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormItemEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormItemId = table.Column<long>(type: "bigint", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormItemEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormItemEvents_FormItems_FormItemId",
                        column: x => x.FormItemId,
                        principalTable: "FormItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormItemEventActions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormItemEventId = table.Column<long>(type: "bigint", nullable: false),
                    ActionId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemEventActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormItemEventActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormItemEventActions_FormItemEvents_FormItemEventId",
                        column: x => x.FormItemEventId,
                        principalTable: "FormItemEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormItemEventActionCallHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormItemEventActionId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    RequestJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemEventActionCallHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormItemEventActionCallHistories_FormItemEventActions_FormItemEventActionId",
                        column: x => x.FormItemEventActionId,
                        principalTable: "FormItemEventActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "CreationDateTime", "DeletedDateTime", "IsDeleted", "JobName", "ModificationDateTime", "OrderIndex", "ParentId", "UniqueIdentity" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2738), null, false, "OpenDialog", null, 0, null, null },
                    { 2L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2739), null, false, "OpenResponsibleDialog", null, 0, null, null },
                    { 3L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2740), null, false, "OpenPage", null, 0, null, null },
                    { 4L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2741), null, false, "CallExternalApi", null, 0, null, null },
                    { 5L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2742), null, false, "SendResult", null, 0, null, null },
                    { 6L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2743), null, false, "Close", null, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreationDateTime", "DeletedDateTime", "IsDeleted", "ModificationDateTime", "Name", "UniqueIdentity" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2704), null, false, null, "Click", null },
                    { 2L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2719), null, false, null, "TextChanged", null },
                    { 3L, new DateTime(2024, 1, 7, 9, 41, 37, 313, DateTimeKind.Local).AddTicks(2721), null, false, null, "ItemSelected", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_CreationDateTime",
                table: "Actions",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_DeletedDateTime",
                table: "Actions",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_IsDeleted",
                table: "Actions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ModificationDateTime",
                table: "Actions",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ParentId",
                table: "Actions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_UniqueIdentity",
                table: "Actions",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreationDateTime",
                table: "Events",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Events_DeletedDateTime",
                table: "Events",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IsDeleted",
                table: "Events",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ModificationDateTime",
                table: "Events",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UniqueIdentity",
                table: "Events",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemActionJobs_ActionId",
                table: "FormItemActionJobs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemActionJobs_CreationDateTime",
                table: "FormItemActionJobs",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemActionJobs_DeletedDateTime",
                table: "FormItemActionJobs",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemActionJobs_FormItemId",
                table: "FormItemActionJobs",
                column: "FormItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemActionJobs_IsDeleted",
                table: "FormItemActionJobs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemActionJobs_ModificationDateTime",
                table: "FormItemActionJobs",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemActionJobs_UniqueIdentity",
                table: "FormItemActionJobs",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActionCallHistories_CreationDateTime",
                table: "FormItemEventActionCallHistories",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActionCallHistories_DeletedDateTime",
                table: "FormItemEventActionCallHistories",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActionCallHistories_FormItemEventActionId",
                table: "FormItemEventActionCallHistories",
                column: "FormItemEventActionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActionCallHistories_IsDeleted",
                table: "FormItemEventActionCallHistories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActionCallHistories_ModificationDateTime",
                table: "FormItemEventActionCallHistories",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActionCallHistories_UniqueIdentity",
                table: "FormItemEventActionCallHistories",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_ActionId",
                table: "FormItemEventActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_CreationDateTime",
                table: "FormItemEventActions",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_DeletedDateTime",
                table: "FormItemEventActions",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_FormItemEventId",
                table: "FormItemEventActions",
                column: "FormItemEventId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_IsDeleted",
                table: "FormItemEventActions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_ModificationDateTime",
                table: "FormItemEventActions",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEventActions_UniqueIdentity",
                table: "FormItemEventActions",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEvents_CreationDateTime",
                table: "FormItemEvents",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEvents_DeletedDateTime",
                table: "FormItemEvents",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEvents_EventId",
                table: "FormItemEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEvents_FormItemId",
                table: "FormItemEvents",
                column: "FormItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEvents_IsDeleted",
                table: "FormItemEvents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEvents_ModificationDateTime",
                table: "FormItemEvents",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemEvents_UniqueIdentity",
                table: "FormItemEvents",
                column: "UniqueIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormItemActionJobs");

            migrationBuilder.DropTable(
                name: "FormItemEventActionCallHistories");

            migrationBuilder.DropTable(
                name: "FormItemEventActions");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "FormItemEvents");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropColumn(
                name: "LocalVariableName",
                table: "FormItems");
        }
    }
}
