using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SarprasHelpdesk.Data;
using SarprasHelpdesk.Hubs;
using SarprasHelpdesk.Repositories;
using SarprasHelpdesk.Services;

var builder = WebApplication.CreateBuilder(args);

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
// Add services to the container.
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILaporanRepository, LaporanRepository>();
builder.Services.AddScoped<ILaporanService, LaporanService>();
builder.Services.AddSignalR();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://+:{port}");

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();   
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Laporan}/{action=Index}/{id?}")
    // name: "default",
    // pattern: "{controller=Laporan}/{action=Create}/{id?}")
    .WithStaticAssets();
    app.MapHub<NotificationHub>("/noitifactionHub");
app.Run();
