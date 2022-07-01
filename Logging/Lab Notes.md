# Entity Framework Core 日誌 Logging 與 效能除錯 練習筆記

## EF09 - 使用 LoggerFactory 觀察送出的 SQL Statement

* 複製 [EF07] 專案到另外一個目錄下
* 修正這個方案為 EF09
* 滑鼠右擊 [EF07] 專案內的 [相依性] 節點
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












## EF10 - 在 ASP.NET Core 專案內，觀察送出的 SQL 敘述

* 開啟 Visual Studio 2022
* 選擇右下角的 [建立新的專案] 按鈕
* 在上方過濾下拉選單的 [所有語言] ，選擇 [C#]
* 在上方過濾下拉選單的 [所有專案類型] ，選擇 [Web]
* 在中間清單區域，找到並且點選 [空的 ASP.NET Core] 這個專案範本
* 點選右下角的 [下一步] 按鈕
* 在 [設定新的專案] 對話窗中，在專案名稱內輸入 `EF10`
* 點選右下角的 [下一步] 按鈕
* 在 [其他資訊] 對話窗中，點選右下角的 [建立] 按鈕
* 當這個專案建立完成後
* 滑鼠右擊 [EF10] 專案內的 [相依性] 節點
* 從彈出功能表中選擇 [管理 NuGet 套件]
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore.SqlServer] 套件
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore.Tools] 套件
* 將 [EF07] 專案內的 [Course.cs]、[Department.cs]、[OfficeAssignment.cs]、[OnsiteCourse.cs]、[Outline.cs]、[Person.cs]、[SchoolContext.cs]、[StudentGrade.cs] 這些檔案，複製到 [EF10] 專案根目錄下
* 在專案根目錄下找到 [Program.cs] 檔案
* 使用底下程式碼將其替換

```csharp
using EF07;

var builder = WebApplication.CreateBuilder(args);

#region 註冊 EF Core 會用到的服務
builder.Services.AddDbContext<SchoolContext>();
#endregion

var app = builder.Build();

#region 開始使用 EF Core
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<SchoolContext>();
Console.WriteLine($"取得 StudentGrade 第一筆紀錄");
var aStudentGrade = context.StudentGrades.FirstOrDefault();
Console.WriteLine($"更新成績為 4.99");
aStudentGrade.Grade = 4.99m;
context.SaveChanges();
#endregion

app.MapGet("/", () => "Hello World!");

app.Run();
```

* 執行這個專案後，查看輸出內容











