using EF12.Models;
using Microsoft.EntityFrameworkCore;

namespace EF13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();

            #region 重新建立這個資料庫
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            #endregion

            #region 新增紀錄
            Department department1 = new Department()
            {
                Name = "新增的科系1",
            };
            context.Department.Add(department1);

            WatchChangeTracking(context, "新增紀錄");
            context.SaveChanges();
            CleanChangeTracking(context);

            #endregion

            #region 更新紀錄 - 呼叫 Update 方法
            department1.Name = "呼叫 Update 方法";
            context.Department.Update(department1);

            WatchChangeTracking(context, "呼叫 Update 方法");
            context.SaveChanges();

            #region 若將這行註解起來，會發生甚麼問題呢？
            CleanChangeTracking(context);
            #endregion

            Console.WriteLine($"請查看資料庫內 Department 資料表內，剛剛更新紀錄是否已經再次被更新");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

            #region 更新紀錄 - 直接更新該執行個體的屬性值
            department1.Name = "直接更新該執行個體的屬性值";

            WatchChangeTracking(context, "直接更新該執行個體的屬性值");
            context.SaveChanges();
            CleanChangeTracking(context);

            Console.WriteLine($"請查看資料庫內 Department 資料表內，剛剛新增紀錄是否已經被更新");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

        }

        #region 觀察 變更追蹤 Change Tracking 變化
        private static void WatchChangeTracking(DataContext context, string message)
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

        static void CleanChangeTracking(DataContext context)
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