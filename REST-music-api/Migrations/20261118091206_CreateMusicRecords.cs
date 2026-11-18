using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REST_music_api.Migrations
{
    /// <inheritdoc />
    public partial class CreateMusicRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicRecords", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MusicRecords",
                columns: new[] { "Id", "Artist", "Duration", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 1, "The Weeknd", 420, 2019, "Blinding Lights" },
                    { 2, "Dua Lipa", 200, 2020, "Levitating" },
                    { 3, "Dua Lipa", 4724, 2020, "Levitating" },
                    { 4, "Ed Sheeran", 69, 2017, "Shape of You" },
                    { 5, "Billie Eilish", 194, 2019, "Bad Guy" },
                    { 6, "Harry Styles", 242, 2022, "As It Was" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicRecords");
        }
    }
}
