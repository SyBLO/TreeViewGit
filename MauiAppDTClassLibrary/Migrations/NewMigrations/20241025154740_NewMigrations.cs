using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiAppDTClassLibrary.Migrations.NewMigrations
{
    /// <inheritdoc />
    public partial class NewMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FullPath = table.Column<string>(type: "TEXT", nullable: false),
                    IsDirectory = table.Column<bool>(type: "INTEGER", nullable: false),
                    FileItemId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileItem_FileItem_FileItemId",
                        column: x => x.FileItemId,
                        principalTable: "FileItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IndexedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    DateIndexed = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexedFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileItem_FileItemId",
                table: "FileItem",
                column: "FileItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileItem");

            migrationBuilder.DropTable(
                name: "IndexedFiles");
        }
    }
}
