using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ICourses.Migrations
{
    public partial class Secondnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Images_ImageId",
                table: "Modules");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Modules_ImageId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Courses");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Modules",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ImageId",
                table: "Modules",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId",
                table: "Courses",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Images_ImageId",
                table: "Modules",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
