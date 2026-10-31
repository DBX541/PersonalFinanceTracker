using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 注册 DbContext (这里为了演示用内存库，实际生产用 PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("FinanceDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();