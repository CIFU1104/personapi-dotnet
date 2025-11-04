using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonaDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PersonaDb")));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
