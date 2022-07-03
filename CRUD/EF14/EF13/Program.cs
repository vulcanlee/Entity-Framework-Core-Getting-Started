using EF12.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

            WatchChangeTracking(context, "沒有任何紀錄異動之前");

            #region 新增紀錄的方法之一
            Department department1 = new Department()
            {
                Name = "新增的科系1",
            };
            context.Department.Add(department1);
            #endregion

            WatchChangeTracking(context, "使用 Add 方法，新增一筆紀錄");

            #region 新增紀錄的方法之二
            Department department2 = new Department()
            {
                Name = "新增的科系2",
            };
            context.Entry(department2).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            #endregion

            WatchChangeTracking(context, "使用變更 State 值，新增一筆紀錄");

            #region 新增記錄到資料庫內
            context.SaveChanges();
            #endregion

            WatchChangeTracking(context, "將記錄新增到資料庫後的狀態");
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
        #endregion
    }
}