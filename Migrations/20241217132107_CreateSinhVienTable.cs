using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kiemtragiuaky.Migrations
{
    public partial class CreateSinhVienTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_NganhHoc_NganhHocMaNganh1",
                table: "SinhVien");

            migrationBuilder.DropIndex(
                name: "IX_SinhVien_NganhHocMaNganh1",
                table: "SinhVien");

            migrationBuilder.DropColumn(
                name: "NganhHocMaNganh1",
                table: "SinhVien");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NganhHocMaNganh1",
                table: "SinhVien",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_NganhHocMaNganh1",
                table: "SinhVien",
                column: "NganhHocMaNganh1");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_NganhHoc_NganhHocMaNganh1",
                table: "SinhVien",
                column: "NganhHocMaNganh1",
                principalTable: "NganhHoc",
                principalColumn: "MaNganh");
        }
    }
}
