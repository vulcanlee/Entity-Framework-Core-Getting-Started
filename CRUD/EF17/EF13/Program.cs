using EF12.Models;

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
            context.SaveChanges();
            Console.WriteLine($"請查看資料庫內 Department 資料表內，是否有這筆紀錄新增");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

            #region 更新紀錄 - 直接更新該執行個體的屬性值
            department1.Name = "直接更新該執行個體的屬性值";
            context.SaveChanges();

            Console.WriteLine($"請查看資料庫內 Department 資料表內，剛剛新增紀錄是否已經被更新");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

            #region 更新紀錄 - 呼叫 Update 方法
            department1.Name = "呼叫 Update 方法";
            context.Department.Update(department1);
            context.SaveChanges();

            Console.WriteLine($"請查看資料庫內 Department 資料表內，剛剛更新紀錄是否已經再次被更新");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

            #region 更新紀錄 - 變更追蹤狀態為 EntityState.Modified
            department1.Name = "變更追蹤狀態為 EntityState.Modified";
            context.Entry(department1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            Console.WriteLine($"請查看資料庫內 Department 資料表內，剛剛更新紀錄是否已經再次被更新");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

        }
    }
}