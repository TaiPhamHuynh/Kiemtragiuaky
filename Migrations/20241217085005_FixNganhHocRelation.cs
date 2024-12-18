using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kiemtragiuaky.Migrations
{
    public partial class FixNganhHocRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocPhan",
                columns: table => new
                {
                    MaHP = table.Column<string>(type: "text", nullable: false),
                    TenHP = table.Column<string>(type: "text", nullable: false),
                    SoTinChi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhan", x => x.MaHP);
                });

            migrationBuilder.CreateTable(
                name: "NganhHoc",
                columns: table => new
                {
                    MaNganh = table.Column<string>(type: "text", nullable: false),
                    TenNganh = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NganhHoc", x => x.MaNganh);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    MaSV = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoTen = table.Column<string>(type: "text", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GioiTinh = table.Column<string>(type: "text", nullable: false),
                    Hinh = table.Column<string>(type: "text", nullable: false),
                    MaNganh = table.Column<string>(type: "text", nullable: false),
                    NganhHocMaNganh1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.MaSV);
                    table.ForeignKey(
                        name: "fk_MaNganh",
                        column: x => x.MaNganh,
                        principalTable: "NganhHoc",
                        principalColumn: "MaNganh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinhVien_NganhHoc_NganhHocMaNganh1",
                        column: x => x.NganhHocMaNganh1,
                        principalTable: "NganhHoc",
                        principalColumn: "MaNganh");
                });

            migrationBuilder.CreateTable(
                name: "DangKy",
                columns: table => new
                {
                    MaDK = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NgayDK = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaSV = table.Column<long>(type: "bigint", nullable: false),
                    SinhVienMaSV = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKy", x => x.MaDK);
                    table.ForeignKey(
                        name: "FK_DangKy_SinhVien_SinhVienMaSV",
                        column: x => x.SinhVienMaSV,
                        principalTable: "SinhVien",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDangKy",
                columns: table => new
                {
                    MaDK = table.Column<int>(type: "integer", nullable: false),
                    MaHP = table.Column<int>(type: "integer", nullable: false),
                    HocPhanMaHP = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDangKy", x => new { x.MaDK, x.MaHP });
                    table.ForeignKey(
                        name: "FK_ChiTietDangKy_DangKy_MaDK",
                        column: x => x.MaDK,
                        principalTable: "DangKy",
                        principalColumn: "MaDK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDangKy_HocPhan_HocPhanMaHP",
                        column: x => x.HocPhanMaHP,
                        principalTable: "HocPhan",
                        principalColumn: "MaHP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDangKy_HocPhanMaHP",
                table: "ChiTietDangKy",
                column: "HocPhanMaHP");

            migrationBuilder.CreateIndex(
                name: "IX_DangKy_SinhVienMaSV",
                table: "DangKy",
                column: "SinhVienMaSV");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaNganh",
                table: "SinhVien",
                column: "MaNganh");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_NganhHocMaNganh1",
                table: "SinhVien",
                column: "NganhHocMaNganh1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDangKy");

            migrationBuilder.DropTable(
                name: "DangKy");

            migrationBuilder.DropTable(
                name: "HocPhan");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "NganhHoc");
        }
    }
}
