using NotinoHomework.Application.Interfaces;
using NotinoHomework.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ConverterTypeResolver>();
builder.Services.AddScoped<ConvertService>();

builder.Services.AddScoped(typeof(ILocalFileHandler), typeof(LocalFileHandler));
builder.Services.AddScoped(typeof(IUrlDownloader), typeof(UrlDownloader));
builder.Services.AddScoped(typeof(IEmailService), typeof(EmailService));

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
