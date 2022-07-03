using EF11;
using Microsoft.EntityFrameworkCore;

namespace EF15
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var context = new SchoolContext();

            WatchChangeTracking(context, "沒有任何紀錄查詢之前");

            #region 查詢單一資料表紀錄
            var person1 = await context.People.ToListAsync();
            Console.WriteLine($"共發現到 {person1.Count} 筆記錄");
            #endregion

            WatchChangeTracking(context, "使用 People.ToListAsync() 方法，查詢單一資料表紀錄");
            CleanChangeTracking(context);

            #region 查看單一資料表內部份紀錄
            var person2 = await context.People.Where(x => x.FirstName.StartsWith("R")).ToListAsync();
            Console.WriteLine($"共發現到 {person2.Count} 筆記錄");
            #endregion

            WatchChangeTracking(context, "使用 Where() 方法，查看單一資料表內部份紀錄");
            CleanChangeTracking(context);

            #region 查看連帶關聯的資料表內紀錄
            var course1 = await context.Courses
                .Include(x => x.Department)
                .ToListAsync();
            Console.WriteLine($"共發現到 {course1.Count} 筆記錄");
            #endregion

            WatchChangeTracking(context, "使用 Include，查看連帶關聯的資料表內紀錄");
            CleanChangeTracking(context);

            #region 指定查詢不用變更追蹤
            var course2 = await context.Courses
                .Include(x => x.Department)
                .AsNoTracking()
                .ToListAsync();
            Console.WriteLine($"共發現到 {course2.Count} 筆記錄");
            #endregion

            WatchChangeTracking(context, "指定查詢不用變更追蹤");
            CleanChangeTracking(context);
        }

        #region 觀察 變更追蹤 Change Tracking 變化
        private static void WatchChangeTracking(SchoolContext context, string message)
        {
            Console.WriteLine($"[{message}] 查看 變更追蹤 內的項目");
            var allEntries = context.ChangeTracker.Entries();
            foreach (var entry in allEntries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}," +
                    $"State: {entry.State.ToString()}");
            }
            Console.WriteLine();
        }

        static void CleanChangeTracking(SchoolContext context)
        {
            var allEntries = context.ChangeTracker.Entries();
            foreach (var entry in allEntries)
            {
                entry.State = EntityState.Detached;
            }
        }
        #endregion
    }
}