using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProConsulta.Migrations
{
    /// <inheritdoc />
    public partial class Correcoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Pacientes",
                type: "VARCHAR(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)");

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Medicos",
                type: "NVARCHAR(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(11)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b093c3e-7df6-411d-89e7-3b3c92449d8a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "732d5a8c-2765-4263-a134-95af98dcae37", "AQAAAAIAAYagAAAAEA3N//lGD5rptAIxSX3pebjzUrP/Xyt8HeALzhigJ60ue5VDTDbhB37DifYBQD0MJQ==", "93be265d-4573-4263-89af-fd43551de419" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Pacientes",
                type: "VARCHAR(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(12)");

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Medicos",
                type: "NVARCHAR(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(12)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b093c3e-7df6-411d-89e7-3b3c92449d8a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fc18552-371e-4047-a97e-d04382085b98", "AQAAAAIAAYagAAAAECuiH0MdHNHaTOAoQLxSp55oGZLLZAiNaqdGcyjLr2AAHCjAxFdXx2vTfAkFZuZryQ==", "b310b524-2b3d-4f8c-ab56-44ea4e0fa30f" });
        }
    }
}
