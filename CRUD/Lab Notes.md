# Entity Framework Core 紀錄的存取 CRUD 練習筆記

## EF13 - EF Core 紀錄新增 Create

* 建立 主控台專案 EF13

  > 勾選 [Do not use top-level statements]

* 滑鼠右擊方案節點 [EF13]
* 從彈出功能表中選擇 [加入] > [現有專案]
* 從 [Managing-Schemas] 資料夾中，在 [EF12] 資料夾內，找到 [EF12.csproj] 這個檔案
* 點選右下角的 [開啟] 按鈕
* 滑鼠右擊 [EF13] 專案內的 [相依性] 節點
* 點選 [新增專案參考] 選項
* 在 [參考管理員] 對話窗內，勾選 [EF12] 這個專案
* 點選 [確定] 按鈕
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
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

            #region 新增紀錄的方法之一
            Department department1 = new Department()
            {
                Name = "新增的科系1",
            };
            context.Department.Add(department1);
            #endregion

            #region 新增紀錄的方法之二
            Department department2 = new Department()
            {
                Name = "新增的科系2",
            };
            context.Entry(department2).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            #endregion

            #region 新增記錄到資料庫內
            context.SaveChanges();
            #endregion
        }
    }
}
```

* 執行這個專案後，檢查 [SchoolCodeFirst] 資料庫內是否存在剛剛新增的紀錄

## EF14 - EF Core 紀錄新增，查看變更追蹤的變化

* 複製專案 [EF13] 成為 [EF14]
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
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
```

* 執行這個專案後，檢查執行結果

## EF15 - EF Core 紀錄查詢 Retrive

* 建立 主控台專案 EF15

  > 勾選 [Do not use top-level statements]

* 滑鼠右擊方案節點 [EF15]
* 從彈出功能表中選擇 [加入] > [現有專案]
* 從 [Managing-Schemas] 資料夾中，在 [EF11] 資料夾內，找到 [EF12.csproj] 這個檔案
* 點選右下角的 [開啟] 按鈕
* 滑鼠右擊 [EF15] 專案內的 [相依性] 節點
* 點選 [新增專案參考] 選項
* 在 [參考管理員] 對話窗內，勾選 [EF11] 這個專案
* 點選 [確定] 按鈕
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
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
```

* 執行這個專案後，檢查執行結果

## EF16 - EF Core 紀錄新增，查看變更追蹤的變化

* 複製專案 [EF15] 成為 [EF16]
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
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
```

* 執行這個專案後，檢查執行結果

## EF17 - EF Core 紀錄更新 Update

* 複製專案 [EF13] 成為 [EF17]
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
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
```

* 執行這個專案後，檢查執行結果

## EF18 - EF Core 紀錄更新，查看變更追蹤的變化與研究

* 複製專案 [EF17] 成為 [EF18]
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
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
```

* 執行這個專案後，檢查執行結果
* 若將第 38 行註解起來 `CleanChangeTracking(context);` ，會發生什麼問題呢？

## EF19 - EF Core 紀錄刪除 Delete

* 複製專案 [EF13] 成為 [EF19]
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
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
```

* 執行這個專案後，檢查執行結果
* 若將第 38 行註解起來 `CleanChangeTracking(context);` ，會發生什麼問題呢？


# aaa










