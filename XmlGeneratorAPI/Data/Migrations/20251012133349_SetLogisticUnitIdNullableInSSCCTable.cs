using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XmlGeneratorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetLogisticUnitIdNullableInSSCCTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SSCCs_LogisticUnits_LogisticUnitId",
                table: "SSCCs");

            migrationBuilder.DropIndex(
                name: "IX_SSCCs_LogisticUnitId",
                table: "SSCCs");

            migrationBuilder.AlterColumn<Guid>(
                name: "LogisticUnitId",
                table: "SSCCs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_SSCCs_LogisticUnitId",
                table: "SSCCs",
                column: "LogisticUnitId",
                unique: true,
                filter: "[LogisticUnitId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SSCCs_LogisticUnits_LogisticUnitId",
                table: "SSCCs",
                column: "LogisticUnitId",
                principalTable: "LogisticUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SSCCs_LogisticUnits_LogisticUnitId",
                table: "SSCCs");

            migrationBuilder.DropIndex(
                name: "IX_SSCCs_LogisticUnitId",
                table: "SSCCs");

            migrationBuilder.AlterColumn<Guid>(
                name: "LogisticUnitId",
                table: "SSCCs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SSCCs_LogisticUnitId",
                table: "SSCCs",
                column: "LogisticUnitId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SSCCs_LogisticUnits_LogisticUnitId",
                table: "SSCCs",
                column: "LogisticUnitId",
                principalTable: "LogisticUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
