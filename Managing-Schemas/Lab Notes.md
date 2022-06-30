# Entity Framework Core Managing-Schemas 練習筆記

## EF07 - 使用資料庫反向工程，取得 EF Core 的資料模型

* 建立 主控台專案 EF07

  > 勾選 [Do not use top-level statements]

* 滑鼠右擊 [EF07] 專案內的 [相依性] 節點
* 從彈出功能表中選擇 [管理 NuGet 套件]
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore.SqlServer] 套件
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore.Tools] 套件
* 若要進行資料庫反向工程，取得 EF Core 的資料模型工作，請點選功能表 [工具] > [NuGet 套件管理員] > [套件管理器主控台]
* 輸入底下指令

  > Scaffold-DbContext "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School" Microsoft.EntityFrameworkCore.SqlServer 













