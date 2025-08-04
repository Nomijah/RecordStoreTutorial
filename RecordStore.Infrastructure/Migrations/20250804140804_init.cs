using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecordStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FormedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CatalogNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    TrackCount = table.Column<int>(type: "int", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumId);
                    table.ForeignKey(
                        name: "FK_Albums_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Albums_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Biography", "Country", "CreatedAt", "FormedDate", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "English rock band formed in Liverpool in 1960", "United Kingdom", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1960, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "The Beatles", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "English rock band formed in London in 1965", "United Kingdom", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1965, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Pink Floyd", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "American jazz trumpeter, bandleader, and composer", "United States", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1944, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Miles Davis", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "French electronic music duo", "France", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1993, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Daft Punk", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "German composer and pianist", "Germany", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1770, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Ludwig van Beethoven", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Rock music genre", "Rock", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Pop music genre", "Pop", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Jazz music genre", "Jazz", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Electronic music genre", "Electronic", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Classical music genre", "Classical", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "AlbumId", "ArtistId", "CatalogNumber", "CreatedAt", "Description", "DurationMinutes", "GenreId", "Id", "Price", "ReleaseDate", "Stock", "Title", "TrackCount", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1, "PCS7088", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "The eleventh studio album by The Beatles", 47, 1, 0, 24.99m, new DateTime(1969, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "Abbey Road", 17, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 2, "SHVL804", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "The eighth studio album by Pink Floyd", 43, 1, 0, 29.99m, new DateTime(1973, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "The Dark Side of the Moon", 9, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 3, "CL1355", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Studio album by Miles Davis", 46, 3, 0, 19.99m, new DateTime(1959, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, "Kind of Blue", 5, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 4, "88883716861", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Fourth studio album by Daft Punk", 74, 4, 0, 27.99m, new DateTime(2013, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, "Random Access Memories", 13, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 5, "OP125", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Beethoven's final complete symphony", 65, 5, 0, 15.99m, new DateTime(1824, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Symphony No. 9", 4, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ArtistId",
                table: "Albums",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_CatalogNumber",
                table: "Albums",
                column: "CatalogNumber",
                unique: true,
                filter: "[CatalogNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_GenreId",
                table: "Albums",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_Name",
                table: "Artists",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
