using EF07;

var builder = WebApplication.CreateBuilder(args);

#region ���U EF Core �|�Ψ쪺�A��
builder.Services.AddDbContext<SchoolContext>();
#endregion

var app = builder.Build();

#region �}�l�ϥ� EF Core
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<SchoolContext>();
Console.WriteLine($"���o StudentGrade �Ĥ@������");
var aStudentGrade = context.StudentGrades.FirstOrDefault();
Console.WriteLine($"��s���Z�� 4.99");
aStudentGrade.Grade = 4.99m;
context.SaveChanges();
#endregion

app.MapGet("/", () => "Hello World!");

app.Run();
