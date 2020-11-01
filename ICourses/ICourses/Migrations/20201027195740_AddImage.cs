using Microsoft.EntityFrameworkCore.Migrations;

namespace ICourses.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Modules",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ImageId",
                table: "Modules",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId",
                table: "Courses",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Images_ImageId",
                table: "Modules",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Images_ImageId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_ImageId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Courses");
        }
    }
}
