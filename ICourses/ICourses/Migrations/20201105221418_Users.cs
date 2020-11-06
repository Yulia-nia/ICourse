using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ICourses.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Courses",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                newName: "IX_Courses_AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorID",
                table: "Courses",
                column: "AuthorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorID",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Courses",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_AuthorID",
                table: "Courses",
                newName: "IX_Courses_AuthorId");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorID",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorId",
                table: "Courses",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
