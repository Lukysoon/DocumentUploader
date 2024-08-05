using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentService.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTag_Documents_documentsId",
                table: "DocumentTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTag",
                table: "DocumentTag");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTag_documentsId",
                table: "DocumentTag");

            migrationBuilder.RenameColumn(
                name: "documentsId",
                table: "DocumentTag",
                newName: "DocumentsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTag",
                table: "DocumentTag",
                columns: new[] { "DocumentsId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTag_TagsId",
                table: "DocumentTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTag_Documents_DocumentsId",
                table: "DocumentTag",
                column: "DocumentsId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTag_Documents_DocumentsId",
                table: "DocumentTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTag",
                table: "DocumentTag");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTag_TagsId",
                table: "DocumentTag");

            migrationBuilder.RenameColumn(
                name: "DocumentsId",
                table: "DocumentTag",
                newName: "documentsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTag",
                table: "DocumentTag",
                columns: new[] { "TagsId", "documentsId" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTag_documentsId",
                table: "DocumentTag",
                column: "documentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTag_Documents_documentsId",
                table: "DocumentTag",
                column: "documentsId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
