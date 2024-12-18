using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProConsulta.Migrations
{
    /// <inheritdoc />
    public partial class UeduUeu4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b093c3e-7df6-411d-89e7-3b3c92449d8a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fc18552-371e-4047-a97e-d04382085b98", "AQAAAAIAAYagAAAAECuiH0MdHNHaTOAoQLxSp55oGZLLZAiNaqdGcyjLr2AAHCjAxFdXx2vTfAkFZuZryQ==", "b310b524-2b3d-4f8c-ab56-44ea4e0fa30f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b093c3e-7df6-411d-89e7-3b3c92449d8a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3357945e-35a1-48d5-b48b-ab73f8acebad", "AQAAAAIAAYagAAAAEPlsDO4adxDbsfPPn3w4SXpqXf5WMkZ4snK235B10pufoMlysHoSZ6SizrTIXSIsxw==", "dc22cc09-858b-49d7-9f22-fb69e958ec15" });
        }
    }
}
