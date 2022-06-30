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

* 打開 [Program.cs] 檔案，修改成為如下程式碼

```csharp
using Microsoft.EntityFrameworkCore;

namespace EF07
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SchoolContext context = new SchoolContext();
            var people = await context.People
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Where(x => x.LastName == "Li")
                .ToListAsync();

            var foo = context.People
                .OrderBy(x => x.LastName);
            foo = foo.ThenBy(z => z.FirstName);
            var bar = foo.Where(x => x.LastName == "Li");
            var bar1 = bar.ToList();
            foreach (var item in people)
            {
                Console.WriteLine($"人員:{item.LastName} {item.FirstName}");
            }
        }
    }
}
```

* 執行這個專案，並且查看結果是否為

```
人員:Li Yan
```












## EF08 - 用 Code First 建立 Entity Framework Core 應用專案

* 建立 主控台專案 EF08

  > 勾選 [Do not use top-level statements]

* 滑鼠右擊 [EF08] 專案內的 [相依性] 節點
* 從彈出功能表中選擇 [管理 NuGet 套件]
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore.SqlServer] 套件
* 搜尋並且安裝 [Microsoft.EntityFrameworkCore.Tools] 套件
* 準備進行 Entity Framework Core 的模型設計
* 滑鼠右擊 [EF08] 專案節點，點選 [加入] > [新增資料夾]
* 在此新資料夾輸入 [Models] 名稱
* 滑鼠右擊 [Models] 資料夾節點
* 點選 [加入] > [類別] > 檔案名稱輸入 [Course]
* 使用底下程式碼替換這個檔案內容

```csharp
namespace EF08.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<StudentGrade> StudentGrades { get; set; }=
            new HashSet<StudentGrade>();
    }
}
```

* 滑鼠右擊 [Models] 資料夾節點
* 點選 [加入] > [類別] > 檔案名稱輸入 [Department]
* 使用底下程式碼替換這個檔案內容

```csharp
namespace EF08.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }=
            new HashSet<Course>();
    }
}
```

* 滑鼠右擊 [Models] 資料夾節點
* 點選 [加入] > [類別] > 檔案名稱輸入 [Student]
* 使用底下程式碼替換這個檔案內容

```csharp
namespace EF08.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudentAddress Address { get; set; }
        public ICollection<StudentGrade> StudentGrades { get; set; } =
            new HashSet<StudentGrade>();
    }
}
```

* 滑鼠右擊 [Models] 資料夾節點
* 點選 [加入] > [類別] > 檔案名稱輸入 [StudentAddress]
* 使用底下程式碼替換這個檔案內容

```csharp
namespace EF08.Models
{
    public class StudentAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
```

* 滑鼠右擊 [Models] 資料夾節點
* 點選 [加入] > [類別] > 檔案名稱輸入 [StudentGrade]
* 使用底下程式碼替換這個檔案內容

```csharp
using Microsoft.EntityFrameworkCore;

namespace EF08.Models
{
    public class StudentGrade
    {
        public int Id { get; set; }
        [Precision(5, 2)]
        public decimal Grade { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
```

* 滑鼠右擊 [Models] 資料夾節點
* 點選 [加入] > [類別] > 檔案名稱輸入 [DataContext]
* 使用底下程式碼替換這個檔案內容

```csharp
using Microsoft.EntityFrameworkCore;

namespace EF08.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentGrade> StudentGrade { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Department> Department { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SchoolCodeFirst");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
```

* 打開 [Program.cs] 檔案，修改成為如下程式碼

```csharp
using EF08.Models;

namespace EF08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
```

* 執行這個專案後，檢查所產生的資料庫結構是否正確











