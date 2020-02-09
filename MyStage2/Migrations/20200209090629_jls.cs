using Microsoft.EntityFrameworkCore.Migrations;

namespace MyStage2.Migrations
{
    public partial class jls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announsment_Users_UserId",
                table: "Announsment");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Announsment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announsment_Users_UserId",
                table: "Announsment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announsment_Users_UserId",
                table: "Announsment");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Announsment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Announsment_Users_UserId",
                table: "Announsment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
