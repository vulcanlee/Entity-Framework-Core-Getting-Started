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

            //#region 建立 DbContextOptions<T> 物件
            //var builder = new DbContextOptionsBuilder<MigrationLabContext>();
            //builder.UseSqlServer(connectionString);
            //#endregion

            //#region 取得 DbContext 並且存取資料庫
            //var context = new MigrationLabContext(builder.Options);
            //Grade grade = new Grade() { GradeName = "MATH3", Section = "B1" };
            //context.Grade.Add(grade);
            //context.SaveChanges();
            //#endregion
            #endregion

            #region 使用相依性注入容器來操作
            #region 建立 DI Container 物件
            var services = new ServiceCollection();
            services.AddDbContext<MigrationLabContext>(options => options.UseSqlServer(connectionString));
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            #endregion

            #region 取得 DbContext 並且存取資料庫
            var context = serviceProvider.GetService<MigrationLabContext>();
            Grade grade = new Grade() { GradeName = "MIS1", Section = "A1" };
            context.Grade.Add(grade);
            context.SaveChanges();
            #endregion
            #endregion

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }

    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }

    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
    public class MigrationLabContext : DbContext
    {
        public MigrationLabContext(DbContextOptions<MigrationLabContext> options)
               : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    string connectionString = @"Data Source=(localdb)\.;Database=MigrationDB";
            //    optionsBuilder.UseSqlServer(connectionString);
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities
        public DbSet<Student> Student { get; set; }
        public DbSet<Grade> Grade { get; set; }
    }

}