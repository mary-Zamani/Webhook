using HangfireApp.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
// Add AutoMapper services
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add services to the container.
builder.Services.AddDbContext<DataBaseContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddControllers();
builder.Services.AddSingleton<Dictionary<int, string>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
 

app.Run();
