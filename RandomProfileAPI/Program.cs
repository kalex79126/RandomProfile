using Microsoft.EntityFrameworkCore;
using RandomProfile.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;
using RandomProfileAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//To give access to IHttpContextAccessor for Audit Data with IAuditable
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<RandomProfileContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RandomProfileContext")));

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
//    app.UseSwagger();
//    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RandomProfileInitializer.Seed(app);

app.Run();
