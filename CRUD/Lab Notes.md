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

## EF14 - EF Core 紀錄新增 Create

* 複製專案 [EF13] 成為 [EF14]
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

# aaa










