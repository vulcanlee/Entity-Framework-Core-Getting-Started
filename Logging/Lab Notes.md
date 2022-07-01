# Entity Framework Core 日誌 Logging 與 效能除錯 練習筆記

## EF09 - 使用 LoggerFactory 觀察送出的 SQL Statement

* 複製 [EF07] 專案到另外一個目錄下
* 修正這個方案為 EF09
* 滑鼠右擊 [EF09] 專案內的 [相依性] 節點
* 從彈出功能表中選擇 [管理 NuGet 套件]
* 搜尋並且安裝 [Microsoft.Extensions.Logging.Console] 套件
* 打開 [Program.cs] 檔案，修改成為如下程式碼

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EF07
{
    internal class Program
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School";
            DbContextOptions<SchoolContext> options = new DbContextOptionsBuilder<SchoolContext>()
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new SchoolContext(options))
            {
                Console.WriteLine($"取得 StudentGrade 第一筆紀錄");
                var aStudentGrade = context.StudentGrades.FirstOrDefault();
                Console.WriteLine($"更新成績為 4.99");
                aStudentGrade.Grade = 4.99m;
                context.SaveChanges();
            }
        }
    }
}
```

* 執行這個專案後，查看輸出內容











