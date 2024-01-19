using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PictureFile = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2983), new TimeSpan(0, 2, 0, 0, 0)), "Nike", null },
                    { 2, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2987), new TimeSpan(0, 2, 0, 0, 0)), "Adidas", null }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2927), new TimeSpan(0, 2, 0, 0, 0)), "Shoes", null },
                    { 2, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2978), new TimeSpan(0, 2, 0, 0, 0)), "Hoodie", null }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "BrandId", "CreatedAt", "Description", "PictureFile", "Price", "Title", "TypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2995), new TimeSpan(0, 2, 0, 0, 0)), "Created for the hardwood but taken to the streets, the '80s b-ball icon returns with classic details and throwback hoops flair. ", "nike-dunk-low-retro-premium.png", 100m, "Nike Dunk Low Retro Premium", 1, null },
                    { 2, 1, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2999), new TimeSpan(0, 2, 0, 0, 0)), "Created for the hardwood but taken to the streets, the '80s b-ball icon returns with crisp leather and and classic \"Panda\" color blocking.", "nike-dunk-mid.png", 120m, "Nike Dunk Mid", 1, null },
                    { 3, 1, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3001), new TimeSpan(0, 2, 0, 0, 0)), "Meet your new go-to hoodie. Heavyweight fleece feels super soft, and the comfy, relaxed fit will have you reaching for it again and again.", "jordan-flight-fleece.png", 100m, "Jordan Flight Fleece", 2, null },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3005), new TimeSpan(0, 2, 0, 0, 0)), "These are not just sneakers, but a real symbol of the era. The adidas Forum model appeared in 1984 and won love not only on basketball courts, but also in show business.", "forum-low.png", 110m, "Forum Low", 1, null },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3007), new TimeSpan(0, 2, 0, 0, 0)), "For your team. For your planet. Created with grassroots football in mind, the Entrada 22 range gives you everything you need to make your game feel and look more beautiful. ", "entrada-22-sweat-hoodie.png", 60m, "ENTRADA 22 SWEAT HOODIE", 2, null },
                    { 6, 1, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3011), new TimeSpan(0, 2, 0, 0, 0)), "Ja Morant became the superstar he is today by repeatedly sinking jumpers on crooked rims, jumping on tractor tires and dribbling through traffic cones in steamy South Carolina summers.", "ja-1.png", 130m, "Ja 1", 1, null },
                    { 7, 1, new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3014), new TimeSpan(0, 2, 0, 0, 0)), "Feel unbeatable, from the tee box to the final putt. Inspired by one of the most iconic sneakers of all time, the Air Jordan 1 G is an instant classic on the course. ", "air-jordan-i-high.png", 190m, "Air Jordan I High G", 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrandId",
                table: "Item",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_TypeId",
                table: "Item",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
