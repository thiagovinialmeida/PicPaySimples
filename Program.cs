using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PicpaySimplesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PicpaySimplesContext") ?? throw new InvalidOperationException("Connection string 'PicpaySimplesContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
