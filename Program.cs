using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Ageofqueenscom.Data;
using Microsoft.Extensions.Configuration;

// SERVICES
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory()
});

Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<DataContext>(options => {
    //string DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "database/age-of-queens.db");
    //options.UseSqlite($"Data Source={DbPath}");
    options.UseMySql(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING"), new MySqlServerVersion(new Version(8, 0, 29)));
});

// APP SETTINGS
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // The default HSTS value is 30 days.
}
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions{ ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run("https://localhost:5001");