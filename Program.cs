using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PicpaySimplesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PicpaySimplesContext") ?? throw new InvalidOperationException("Connection string 'PicpaySimplesContext' not found.")));
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<LojistaService>();

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
