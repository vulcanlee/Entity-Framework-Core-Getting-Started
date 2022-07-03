using EF11;
using Microsoft.EntityFrameworkCore;

namespace EF15
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var context = new SchoolContext();

            #region 查詢單一資料表紀錄
            var person1 = await context.People.ToListAsync();
            Console.WriteLine($"共發現到 {person1.Count} 筆記錄");
            #endregion

            #region 查看單一資料表內部份紀錄
            var person2 = await context.People.Where(x => x.FirstName.StartsWith("R")).ToListAsync();
            Console.WriteLine($"共發現到 {person2.Count} 筆記錄");
            #endregion

            #region 查看連帶關聯的資料表內紀錄
            var course1 = await context.Courses
                .Include(x => x.Department)
                .ToListAsync();
            Console.WriteLine($"共發現到 {course1.Count} 筆記錄");
            #endregion

            #region 指定查詢不用變更追蹤
            var course2 = await context.Courses
                .Include(x => x.Department)
                .AsNoTracking()
                .ToListAsync();
            Console.WriteLine($"共發現到 {course2.Count} 筆記錄");
            #endregion
        }
    }
}