using Microsoft.EntityFrameworkCore;
using Serilog;
using StudentGradeApp.AutoMapper;
using StudentGradeApp.DataContext;
using StudentGradeApp.Repository;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
//   .AddJsonFile("StudentGrade.json", optional: false, reloadOnChange: true)
//   .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<StudentContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddControllers();
builder.Services.AddScoped<IStudentGradeRepository, StudentGradeRepository>();
builder.Services.AddScoped<IPaystackService, PaystackService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
        policy.WithOrigins("https://student-grade-three.vercel.app")
              // policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader());
});
//student-grade-three.vercel.app

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularApp");
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.MapControllers();

app.Run();
