using Microsoft.EntityFrameworkCore.Migrations;

namespace DataIngestion.TestAssignment.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<long>(type: "bigint", nullable: false),
                    IsPrimaryArtist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExportDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActualArtist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionId = table.Column<long>(type: "bigint", nullable: false),
                    Upc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmgAlbumId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionMatches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentalAdvisoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtworkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItunesReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabelStudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Copyright = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompilation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistCollections");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "CollectionMatches");

            migrationBuilder.DropTable(
                name: "Collections");
        }
    }
}
