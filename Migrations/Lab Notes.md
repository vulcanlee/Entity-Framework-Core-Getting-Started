# Entity Framework Core Migration 練習筆記

## EF01 - EF Core 遷移練習前環境準備工作

* 建立 主控台專案 EF01

  > 勾選 [Do not use top-level statements]

* 建立新的 .NET 6.0 專案，名稱為 [Common]

  > 選擇 [類別庫] 專案，用於建立以 .NET 或 .NET Standard 為目標的類別庫

  * 刪除 [Class1.cs] 檔案

* 建立新的資料夾 [DataModels]

* 建立新的 .NET 6.0 專案，名稱為 [Business]

  * 刪除 [Class1.cs] 檔案
* 建立新的資料夾 [Helpers]

* 建立新的 .NET 6.0 專案，名稱為 [Entities]

  * 刪除 [Class1.cs] 檔案
* 建立新的資料夾 [Models]

* 建立新的 .NET 6.0 專案，名稱為 [DataAccess]

  * 刪除 [Class1.cs] 檔案
* 建立新的資料夾 [Services]

## EF02 - 建立 一對多 Entity Model 與 DbContext 並第一次遷移與同步

* 滑鼠右擊 [Entities] 專案內的 [相依性] 節點
* 從彈出功能表中選擇 [管理 NuGet 套件]
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore.SqlServer] 套件

* 在 [Entities] 專案下的 [Models] 資料夾，建立 [OneToMany.cs] 類別

  > https://www.entityframeworktutorial.net/efcore/one-to-many-conventions-entity-framework-core.aspx

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public ICollection<Student> Students { get; set; }=new HashSet<Student>();
    }

    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }

}
```

* 在 [Entities] 專案下的 ，建立 [MigrationLabDbContext.cs] 類別

```csharp
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MigrationLabContext : DbContext
    {
        public MigrationLabContext()
        {

        }

        public MigrationLabContext(DbContextOptions<MigrationLabContext> options)
               : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Data Source=(localdb)\.;Database=MigrationDB";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities
        public DbSet<Student> Student { get; set; }
        public DbSet<Grade> Grade { get; set; }
    }
}
```

* 進行 Entity Model 與 資料庫結構同步

* 在 [主控台專案 EF01] 專案搜尋安裝 [Microsoft.EntityFrameworkCore.SqlServer] 與 [Microsoft.EntityFrameworkCore.Tools] 套件

* 在 [EF01] 專案加入參考 [Entities] 專案

* 在 [EF01] 專案內找到並且打開 [Program.cs] 檔案，替換底下程式碼

```csharp
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EF01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\.;Database=MigrationDB";

            #region 使用 DbContextOptionsBuilder

            #region 建立 DbContextOptions<T> 物件
            var builder = new DbContextOptionsBuilder<MigrationLabContext>();
            builder.UseSqlServer(connectionString);
            #endregion

            #region 取得 DbContext 並且存取資料庫
            var context = new MigrationLabContext(builder.Options);
            Grade grade = new Grade() { GradeName = "MATH3", Section = "B1" };
            context.Grade.Add(grade);
            context.SaveChanges();
            #endregion
            #endregion

            #region 使用相依性注入容器來操作
            //#region 建立 DI Container 物件
            //var services = new ServiceCollection();
            //services.AddDbContext<MigrationLabContext>(options => options.UseSqlServer(connectionString));
            //ServiceProvider serviceProvider = services.BuildServiceProvider();
            //#endregion

            //#region 取得 DbContext 並且存取資料庫
            //var context = serviceProvider.GetService<MigrationLabContext>();
            //Grade grade = new Grade() { GradeName = "MIS1", Section = "A1" };
            //context.Grade.Add(grade);
            //context.SaveChanges();
            //#endregion
            #endregion

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
```

* 若要進行遷移工作，請點選功能表 [工具] > [NuGet 套件管理員] > [套件管理器主控台]
  * 輸入底下指令

    > Add-Migration MyFirstMigration -StartupProject EF01 -Project Entities -Context MigrationLabContext

* 若要還原剛剛的遷移工作，請下達底下指令

  > Remove-Migration -StartupProject EF01 -Project Entities -Context MigrationLabContext

* 一旦在 [Migrations] 資料夾內有檔案產生出來後，便可以進行資料庫同步，請下達底下指令

  > Update-Database -StartupProject EF01 -Project Entities -Context MigrationLabContext

* 若要產生資料庫遷移的 SQL 腳本，可以使用底下指令

  > Script-Migration -StartupProject EF01 -Project Entities -Context MigrationLabContext -Idempotent

## EF03 - 新建 一對一 Entity Model 並進行第 2 次遷移與同步

* 在 [Entities] 專案下的 [Models] 資料夾，建立 [OneToOne.cs] 檔案，程式碼如下

  > https://www.tektutorialshub.com/entity-framework-core/ef-core-one-to-one-relationship/

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation property Returns the Employee Address
        public EmployeeAddress EmployeeAddress { get; set; }
    }

    public class EmployeeAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public int EmployeeID { get; set; }
        //Navigation property Returns the Employee object
        public Employee Employee { get; set; }
    }
}
```

* 在 [Entities] 專案下找到 [MigrationLabContext.cs] 並打開該檔案
* 搜尋到 `public DbSet<Grade> Grade { get; set; }` 敘述，在其下方加入底下的程式碼

```csharp
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }
```

* 進行遷移工作，請點選功能表 [工具] > [NuGet 套件管理員] > [套件管理器主控台]
  * 輸入底下指令

    > Add-Migration AddNewEntityModel -StartupProject EF01 -Project Entities -Context MigrationLabContext

* 若要還原剛剛的遷移工作，請下達底下指令

  > Remove-Migration -StartupProject EF01 -Project Entities -Context MigrationLabContext

* 一旦在 [Migrations] 資料夾內有新檔案產生出來後，便可以進行資料庫同步，請下達底下指令

  > Update-Database -StartupProject EF01 -Project Entities -Context MigrationLabContext


## EF04 - 新建 多對多 Entity Model 並進行第 3 次遷移與同步

* 在 [Entities] 專案下的 [Models] 資料夾，建立 [ManyToMany.cs] 檔案，程式碼如下

  > https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
    public class BookCategory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
```

* 在 [Entities] 專案下找到 [MigrationLabContext.cs] 並打開該檔案
* 搜尋到 `public DbSet<EmployeeAddress> EmployeeAddress { get; set; }` 敘述，在其下方加入底下的程式碼

```csharp
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
```

* 進行遷移工作，請點選功能表 [工具] > [NuGet 套件管理員] > [套件管理器主控台]
  * 輸入底下指令

    > Add-Migration AddManyToManyEntityModel -StartupProject EF01 -Project Entities -Context MigrationLabContext

* 若要還原剛剛的遷移工作，請下達底下指令

  > Remove-Migration -StartupProject EF01 -Project Entities -Context MigrationLabContext

* 一旦在 [Migrations] 資料夾內有新檔案產生出來後，便可以進行資料庫同步，請下達底下指令

  > Update-Database -StartupProject EF01 -Project Entities -Context MigrationLabContext









