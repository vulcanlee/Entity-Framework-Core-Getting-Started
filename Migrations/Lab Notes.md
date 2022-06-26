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

## EF02 - 建立 Entity Model 與 DbContext

* 滑鼠右擊 [Entities] 專案內的 [相依性] 節點
* 從彈出功能表中選擇 [管理 NuGet 套件]
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore] 套件

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

* 在 [Entities] 專案下的 [Models] 資料夾，建立 [OneToOne.cs] 類別

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

* 在 [Entities] 專案下的 [Models] 資料夾，建立 [ManyToMany.cs] 類別

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
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities
        public DbSet<Student> Student { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
    }
}
```

