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

            #region 刪除紀錄 - 取得該 Entity 的執行個體，要有主鍵值
            context.Remove(department1);
            context.SaveChanges();

            Console.WriteLine($"請查看資料庫內 Department 資料表內，剛剛新增紀錄是否已經被刪除");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

        }
    }
}