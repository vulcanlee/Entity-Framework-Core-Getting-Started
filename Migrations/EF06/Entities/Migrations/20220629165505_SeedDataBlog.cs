using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class SeedDataBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "Name" },
                values: new object[] { "ASP.NET Core" });
            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "Name" },
                values: new object[] { "Blazor Server" });
            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "Name" },
                values: new object[] { "MAUI" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}
