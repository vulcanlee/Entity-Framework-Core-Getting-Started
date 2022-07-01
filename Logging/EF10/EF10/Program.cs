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
