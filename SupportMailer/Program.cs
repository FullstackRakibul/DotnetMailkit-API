using Microsoft.EntityFrameworkCore;
using SupportMailer.Configuration;
using SupportMailer.Models;
using SupportMailer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// config Dependency Injection DB

builder.Services.AddDbContext<SupportMailerContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));

// mailkit 

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

// config Dependency Injection Mailkit

builder.Services.AddTransient<IMailService, MailService>();



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
