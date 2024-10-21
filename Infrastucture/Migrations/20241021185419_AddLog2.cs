using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class AddLog2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTracer",
                table: "ActivityTracer");

            migrationBuilder.RenameTable(
                name: "ActivityTracer",
                newName: "ActivityTracker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTracker",
                table: "ActivityTracker",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTracker",
                table: "ActivityTracker");

            migrationBuilder.RenameTable(
                name: "ActivityTracker",
                newName: "ActivityTracer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTracer",
                table: "ActivityTracer",
                column: "Id");
        }
    }
}
