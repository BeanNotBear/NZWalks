using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("36f72fe7-43d6-4e3f-8934-d05b13a0e7de"), "Medium" },
                    { new Guid("9ea58ba1-094a-4d5d-b8d9-39ce0ec0a3f4"), "Hard" },
                    { new Guid("a28f34ab-9898-4a70-b057-fc2a98470aeb"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("36cad70d-0f48-4b63-729e-08dcc742cc2c"), "CN", "China", "https://example.com/images/china.png" },
                    { new Guid("55bb048f-9d92-4cbe-a2dd-08dcc73d9c77"), "AF", "Africa", "https://example.com/images/africa.png" },
                    { new Guid("7d388ef1-33a0-478d-778e-08dcc5f1aee4"), "VN", "Vietnam", "https://example.com/images/vietnam.png" },
                    { new Guid("8a577820-7d28-4bae-a2df-08dcc73d9c77"), "EG", "Egypt", "https://example.com/images/egypt.png" },
                    { new Guid("ac8acc08-94ff-47ff-6862-08dcc349a7bc"), "REG002", "Europe", "http://example.com/region/europe.png" },
                    { new Guid("f79b9c8d-d7e3-4375-9134-24782dbd7f68"), "REG006", "Australia", "http://example.com/region/australia.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("36f72fe7-43d6-4e3f-8934-d05b13a0e7de"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9ea58ba1-094a-4d5d-b8d9-39ce0ec0a3f4"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a28f34ab-9898-4a70-b057-fc2a98470aeb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("36cad70d-0f48-4b63-729e-08dcc742cc2c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("55bb048f-9d92-4cbe-a2dd-08dcc73d9c77"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7d388ef1-33a0-478d-778e-08dcc5f1aee4"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8a577820-7d28-4bae-a2df-08dcc73d9c77"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ac8acc08-94ff-47ff-6862-08dcc349a7bc"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f79b9c8d-d7e3-4375-9134-24782dbd7f68"));
        }
    }
}
