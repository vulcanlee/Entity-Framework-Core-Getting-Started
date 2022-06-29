using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class AddStoreProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"CREATE OR ALTER PROC usp_GetAllStudentByName(@paraStudentName nvarchar(max)) AS SELECT * FROM Student WHERE StudentName like @paraStudentName +'%' ";
            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = "DROP PROC usp_GetAllStudentByName";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
