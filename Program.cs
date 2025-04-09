using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Project.Data;
using Project.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PicpaySimplesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PicpaySimplesTestContext") ?? throw new InvalidOperationException("Connection string 'PicpaySimplesContext' not found.")));
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<LojistaService>();
builder.Services.AddScoped<TransacaoService>();

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
