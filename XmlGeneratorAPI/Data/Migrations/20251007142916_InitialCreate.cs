using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XmlGeneratorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesUpload",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    StoredFileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UploadedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesUpload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogisticUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WeightInKg = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    LengthInCm = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    WidthInCm = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    HeightInCm = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ItemsCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SGTINs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SGTINs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SSCCs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(18)", maxLength: 18, nullable: false),
                    ExtensionDigit = table.Column<string>(type: "VARCHAR(1)", maxLength: 1, nullable: false),
                    Gs1CompanyPrefix = table.Column<string>(type: "VARCHAR(12)", maxLength: 12, nullable: false),
                    SerialNumberPaddedZeros = table.Column<string>(type: "VARCHAR(12)", maxLength: 12, nullable: false),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    CheckDigit = table.Column<string>(type: "VARCHAR(1)", maxLength: 1, nullable: false),
                    LogisticUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSCCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SSCCs_LogisticUnits_LogisticUnitId",
                        column: x => x.LogisticUnitId,
                        principalTable: "LogisticUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogisticUnitsAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogisticUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SgtinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnassignedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticUnitsAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticUnitsAssignments_LogisticUnits_LogisticUnitId",
                        column: x => x.LogisticUnitId,
                        principalTable: "LogisticUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogisticUnitsAssignments_SGTINs_SgtinId",
                        column: x => x.SgtinId,
                        principalTable: "SGTINs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogisticUnitsAssignments_LogisticUnitId",
                table: "LogisticUnitsAssignments",
                column: "LogisticUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticUnitsAssignments_SgtinId",
                table: "LogisticUnitsAssignments",
                column: "SgtinId");

            migrationBuilder.CreateIndex(
                name: "IX_SGTINs_Code",
                table: "SGTINs",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SSCCs_Code",
                table: "SSCCs",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SSCCs_LogisticUnitId",
                table: "SSCCs",
                column: "LogisticUnitId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesUpload");

            migrationBuilder.DropTable(
                name: "LogisticUnitsAssignments");

            migrationBuilder.DropTable(
                name: "SSCCs");

            migrationBuilder.DropTable(
                name: "SGTINs");

            migrationBuilder.DropTable(
                name: "LogisticUnits");
        }
    }
}
