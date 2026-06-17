using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using SarprasHelpdesk.Data;
using SarprasHelpdesk.Hubs;
using SarprasHelpdesk.Repositories;
using SarprasHelpdesk.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<ILaporanRepository, LaporanRepository>();
builder.Services.AddScoped<ILaporanService, LaporanService>();
builder.Services.AddSignalR();
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://+:{port}");

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
