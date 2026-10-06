using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futsal_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddArena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInfos_Arena_ArenaId",
                table: "BookingInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arena",
                table: "Arena");

            migrationBuilder.RenameTable(
                name: "Arena",
                newName: "Arenas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arenas",
                table: "Arenas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInfos_Arenas_ArenaId",
                table: "BookingInfos",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInfos_Arenas_ArenaId",
                table: "BookingInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arenas",
                table: "Arenas");

            migrationBuilder.RenameTable(
                name: "Arenas",
                newName: "Arena");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arena",
                table: "Arena",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInfos_Arena_ArenaId",
                table: "BookingInfos",
                column: "ArenaId",
                principalTable: "Arena",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
